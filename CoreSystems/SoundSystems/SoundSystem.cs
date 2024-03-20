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
        public static FMOD.Studio.System FMODStudioSystem;
        public static FMOD.System FMODCoreSystem;
        private static Dictionary<string, Bank> BankCache = new Dictionary<string, Bank>();

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

        public static void ThrowIfNotOK(RESULT result) // thanks aristurtle youre the goat
        {
            if (result != RESULT.OK)
            {
                throw new InvalidOperationException(Error.String(result));
            }
        }
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

        public static void PlaySound(string path)
        {
            LoadBank("Content/Sounds/secondgamebank.bank");
            ThrowIfNotOK(FMODStudioSystem.getEvent(path, out EventDescription _event));
            ThrowIfNotOK(_event.createInstance(out EventInstance instance));
            ThrowIfNotOK(instance.start());
            instance.release();
        }
    }
}
