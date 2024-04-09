using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using FMOD.Studio;
using FMOD;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
namespace secondgame.Drawing
{
    public class AssetRegistry
    {
        public Dictionary<string, ManagedTexture> Textures;
        /// <summary>
        /// Dictionary containing the name of a file and its file path.
        /// Can be used as a "map" - given the name of a file, it finds the saved path to it.
        /// The key is the name of the file, and the value is the path leading to it.
        /// </summary>
        public Dictionary<string, string> FileNameMap;
        public AssetRegistry()
        {
            Textures = new Dictionary<string, ManagedTexture>();
            FileNameMap = new Dictionary<string, string>();
            PopulateFileNameMap();
        }

        public Texture2D GetTexture(string fileName)
        {
            string path = FileNameMap[fileName];
            Texture2D texture = MainInstance.Content.Load<Texture2D>(path);

            if (Textures.ContainsKey(fileName))
            {
                return Textures[fileName].Asset;
            }
            else
            {
                Textures.Add(fileName, new ManagedTexture(texture));
            }
            return texture;
        }

        public void PopulateFileNameMap()
        {
            string[] files = Directory.GetFiles("Content", "", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                string toSaveAs = files[i];

                while (toSaveAs.Contains("\\")) //remove all slashes
                {
                    int indexOfSlash = toSaveAs.IndexOf("\\");

                    int startIndex = indexOfSlash + 1; //+ 1 accounts for double slash, there's two slashes in the IndexOf cos the first one is to character escape the second

                    toSaveAs = toSaveAs.Substring(startIndex, toSaveAs.Length - startIndex); //only take everything after the double slash
                }

                int indexOfDot = toSaveAs.IndexOf(".");

                string fileExtension = toSaveAs.Substring(indexOfDot, toSaveAs.Length - indexOfDot); // remove file extension

                toSaveAs = toSaveAs.Substring(0, indexOfDot);

                if (fileExtension == ".xnb") //only do this for .xnbs, when this was written credits.txt would crash it as a .txt doesnt work
                {
                    toSaveAs = toSaveAs.Substring(0, indexOfDot); //remove file extension

                    FileNameMap.Add(toSaveAs, RemoveContentPrefix(RemoveFileExtenstion(files[i])));
                }
            }
        }

        public string RemoveFileExtenstion(string path)
        {
            int indexOfDot = path.IndexOf(".");

            return path.Substring(0, indexOfDot);
        }

        public string RemoveContentPrefix(string path)
        {
            int contentLength = MainInstance.Content.RootDirectory.Length + 1; // add one to remove /
            // if the string begins with "Content"
            if (path.Substring(0, contentLength - 1) == "Content")
            {
                // remove "Content\"
                return path.Substring(contentLength, path.Length - contentLength);
            }
            else return path;
        }
    }
}
