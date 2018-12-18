using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2016
{
    public class Day04 : IPuzzle
    {
        public bool Active => true;

        public List<string> ReadInput(string path)
        {
            List<string> inputList = new List<string>();
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                inputList.Add(line);
            }

            return inputList;
        }

        private List<string> GetRealRooms(List<string> inputList)
        {
            List<string> realRooms = new List<string>();
            foreach (var room in inputList)
            {
                List<string> split = room.Split('[').ToList();
                string checkSum = split[1].TrimEnd(']');
                List<string> roomNameSplit = split[0].Split('-').ToList();
                string sectorId = roomNameSplit.Last();

                SortedDictionary<char, int> counting = new SortedDictionary<char, int>();
                for (int i = 0; i < roomNameSplit.Count - 1; i++)
                {
                    foreach (char c in roomNameSplit[i])
                    {
                        int count = 0;
                        if (counting.TryGetValue(c, out count))
                        {
                            count++;
                            counting[c] = count;
                        }
                        else
                        {
                            counting.Add(c, 1);
                        }
                    }
                }

                if (RoomIsValid(counting, checkSum))
                {
                    realRooms.Add(sectorId);
                }

            }
            return realRooms;
        }

        private bool RoomIsValid(SortedDictionary<char, int> counting, string checkSum)
        {
            int previousOccurence = 99999;
            bool realRoom = true;
            char previousChar = 'z';
            foreach (char r in checkSum)
            {
                int value = 0;
                if (!counting.TryGetValue(r, out value))
                {
                    return false;
                }

                if (value > previousOccurence)
                {
                    return false;
                }
                else if (value == previousOccurence)
                {
                    if (r < previousChar)
                    {
                        return false;
                    }
                }
                else
                {
                    previousOccurence = value;
                }
                previousChar = r;
            }
            if (realRoom)
            {
                foreach (var c in counting)
                {
                    if (c.Value > previousOccurence && !checkSum.Contains(c.Key.ToString()))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int GetSectorIdOfRoom(List<string> inputList, string v)
        {
            int sectorId = 0;
            List<string> realRooms = new List<string>();
            foreach (var room in inputList)
            {
                List<string> split = room.Split('[').ToList();
                string checkSum = split[1].TrimEnd(']');
                List<string> roomNameSplit = split[0].Split('-').ToList();
                sectorId = int.Parse(roomNameSplit.Last());

                string name = GetRoomName(sectorId, roomNameSplit.Take(roomNameSplit.Count - 1).ToList());

                if (name.Contains(v))
                {
                    return sectorId;
                }
            }

            return sectorId;
        }

        private string GetRoomName(int sectorId, List<string> enumerable)
        {
            string roomName = "";
            foreach (var word in enumerable)
            {
                foreach (char c in word)
                {
                    roomName += ((char)((((c - 97 + sectorId) % 26)) + 97)).ToString();
                }
                roomName += " ";
            }
            return roomName;
        }

        public string RunOne()
        {
            List<string> inputList = ReadInput(@"..\..\Data\2016\input04.txt");
            List<string> realRooms = GetRealRooms(inputList);

            int sum = realRooms.Sum(x => int.Parse(x));
            return sum.ToString();
        }

        public string RunTwo()
        {
            List<string> inputList = ReadInput(@"..\..\Data\2016\input04.txt");
            int sectorId = GetSectorIdOfRoom(inputList, "pole");
            return sectorId.ToString();
        }
    }
}
