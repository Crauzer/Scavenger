using LeagueToolkit.Helpers.Cryptography;
using LeagueToolkit.Helpers.Hashing;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Scavenger.Utilities
{
    public static class Hashtables
    {
        public static string OBJECTS_FILE = "OBJECTS_HASHTABLE.txt";
        public static string FIELDS_FILE = "FIELDS_HASHTABLE.txt";
        public static string HASHES_FILE = "HASHES_HASHTABLE.txt";
        public static string TYPES_FILE = "TYPES_HASHTABLE.txt";
        public static string WAD_ENTRIES_FILE = "WAD_ENTRIES_HASHTABLE.txt";

        private static Dictionary<uint, string> _objects = new Dictionary<uint, string>();
        private static Dictionary<uint, string> _fields = new Dictionary<uint, string>();
        private static Dictionary<uint, string> _hashes = new Dictionary<uint, string>();
        private static Dictionary<uint, string> _types = new Dictionary<uint, string>();
        private static Dictionary<ulong, string> _wadEntries = new Dictionary<ulong, string>();

        public static void Load()
        {
            _objects = ParseBinHashtable(OBJECTS_FILE);
            _fields = ParseBinHashtable(FIELDS_FILE);
            _hashes = ParseBinHashtable(HASHES_FILE);
            _types = ParseBinHashtable(TYPES_FILE);
            _wadEntries = ParseWadHashtable(WAD_ENTRIES_FILE);
        }

        public static string GetObject(uint key)
        {
            if (_objects.ContainsKey(key))
            {
                return _objects[key];
            }
            else
            {
                return key.ToString("x16");
            }
        }
        public static string GetField(uint key)
        {
            if (_fields.ContainsKey(key))
            {
                return _fields[key];
            }
            else
            {
                return key.ToString("x16");
            }
        }
        public static string GetHash(uint key)
        {
            if (_hashes.ContainsKey(key))
            {
                return _hashes[key];
            }
            else
            {
                return key.ToString("x16");
            }
        }
        public static string GetType(uint key)
        {
            if (_types.ContainsKey(key))
            {
                return _types[key];
            }
            else
            {
                return key.ToString("x16");
            }
        }
        public static string GetWadEntry(ulong key)
        {
            if (_wadEntries.ContainsKey(key))
            {
                return _wadEntries[key];
            }
            else
            {
                return key.ToString("x16");
            }
        }

        private static Dictionary<uint, string> ParseBinHashtable(string filePath)
        {
            Dictionary<uint, string> map = new Dictionary<uint, string>();

            foreach (string line in File.ReadAllLines(filePath))
            {
                string hashString = line.Split(" ")[1];
                uint hash = Fnv1a.HashLower(hashString);

                if (map.ContainsKey(hash) is false)
                {
                    map.Add(hash, hashString);
                }
            }

            return map;
        }
        private static Dictionary<ulong, string> ParseWadHashtable(string filePath)
        {
            Dictionary<ulong, string> map = new Dictionary<ulong, string>();

            foreach (string line in File.ReadAllLines(filePath))
            {
                string hashString = line.Split(" ")[1];
                ulong hash = XXHash.XXH64(Encoding.UTF8.GetBytes(hashString.ToLower()));

                if (map.ContainsKey(hash) is false)
                {
                    map.Add(hash, hashString);
                }
            }

            return map;
        }
    }
}
