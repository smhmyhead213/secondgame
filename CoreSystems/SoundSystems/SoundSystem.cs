using FMOD;
using FMOD.Studio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.CoreSystems.SoundSystems
{
    public static class SoundSystem
    {
        /// <summary>
        /// The FMOD Studio System.
        /// </summary>
        public static FMOD.Studio.System FMODStudioSystem;
        /// <summary>
        /// The FMOD Core System.
        /// </summary>
        public static FMOD.System FMODCoreSystem;
        /// <summary>
        /// A dictionary cache of banks that have already been loaded, to prevent pointless reloading of banks already in memory.
        /// </summary>
        private static Dictionary<string, Bank> BankCache = new Dictionary<string, Bank>();

        /// <summary>
        /// Initialises FMOD. This method must be called in LoadContent() so that FMOD can be used.
        /// </summary>
        public static void SetUpFMOD()
        {
            // call all these methods that have to be called idk

            Memory.GetStats(out _, out _);
            FMOD.Studio.System.create(out var fmodStudioSystem);
            fmodStudioSystem.getCoreSystem(out var fmodSystem);
            FMODStudioSystem = fmodStudioSystem;
            FMODCoreSystem = fmodSystem;
            fmodSystem.setDSPBufferSize(256, 4);
            fmodStudioSystem.initialize(
              128,
              FMOD.Studio.INITFLAGS.NORMAL,
              FMOD.INITFLAGS.NORMAL,
              (IntPtr)0
            );

            // load in master bank, this must be done
            LoadBank("Content/Sounds/Master.bank");

            // load in strings bank so that events can be identified by their path
            LoadBank("Content/Sounds/Master.strings.bank");
        }

        /// <summary>
        /// Wrapper that should be used around any FMOD method so that an exception is thrown when an error occurs. Without this, FMOD can silently crash and fail.
        /// </summary>
        /// <param name="result">The RESULT returned from the method that this method wraps around.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// 
        public static void ThrowIfNotOK(RESULT result) // thanks aristurtle youre the goat
        {
            if (result != RESULT.OK)
            {
                throw new InvalidOperationException(Error.String(result));
            }
        }

        /// <summary>
        /// Loads in and returns a bank from a given file path.
        /// </summary>
        /// <param name="path">The file path to the location of the bank file.</param>
        /// <returns></returns>
        public static Bank LoadBank(string path)
        {
            // check if the cache already contains the needed bank.
            if (BankCache.TryGetValue(path, out Bank bank))
            {
                // if so, do not retrieve bank again and just return the cached one
                return bank;
            }

            // load in the bank file if it isnt cached
            ThrowIfNotOK(FMODStudioSystem.loadBankFile(
            path,
            LOAD_BANK_FLAGS.NORMAL,
            out Bank newBank
            ));

            // add it to the cache if its not already present
            BankCache.Add(path, newBank);

            return newBank;
        }

        /// <summary>
        /// A method that plays a sound given the name of the event that contains it.
        /// </summary>
        /// <param name="soundName">The name of the event containing the sound.</param>

        // technically speaking this method plays an event but each event just contains a sound anyway
        public static void PlaySound(string soundName)
        {
            // load in the bank with all the sounds in it
            LoadBank("Content/Sounds/secondgamebank.bank");
            // attempt to get the event containing the sound and output the event description
            ThrowIfNotOK(FMODStudioSystem.getEvent("event:/" + soundName, out EventDescription _event));
            // create an instance of the event description
            ThrowIfNotOK(_event.createInstance(out EventInstance instance));
            // play the instance/event
            ThrowIfNotOK(instance.start());
            // release it from memory
            instance.release();
        }
    }
}
