using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Days_2020
{
    public class Passport
    {
        public int BirthYear { get; internal set; }
        public int IssueYear { get; internal set; }
        public int ExpirationYear { get; internal set; }
        public string Height { get; internal set; }
        public string HairColor { get; internal set; }
        public string EyeColor { get; internal set; }
        public string PassportId { get; internal set; }
        public string CountryId { get; internal set; }

        public bool IsValid
        {
            get
            {
                return BirthYear != 0 &&
                    IssueYear != 0 &&
                    ExpirationYear != 0 &&
                    !string.IsNullOrEmpty(Height) &&
                    !string.IsNullOrEmpty(HairColor) &&
                    !string.IsNullOrEmpty(EyeColor) &&
                    !string.IsNullOrEmpty(PassportId);
            }
        }
    }

    public class Day04 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<Passport> passports = new List<Passport>();
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input04.txt").ToList();
            Passport p = new Passport();
            passports.Add(p);

            for(int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i] == "")
                {
                    p = new Passport();
                    passports.Add(p);
                }
                else
                {
                    List<string> split = inputs[i].Split(' ').ToList();
                    for (int j = 0; j < split.Count; j++)
                    {
                        string key = split[j].Split(':')[0];
                        string value = split[j].Split(':')[1];
                        switch (key)
                        {
                            case "byr":
                                p.BirthYear = int.Parse(value);
                                break;
                            case "iyr":
                                p.IssueYear = int.Parse(value);
                                break;
                            case "eyr":
                                p.ExpirationYear = int.Parse(value);
                                break;
                            case "hgt":
                                p.Height = value;
                                break;
                            case "hcl":
                                p.HairColor = value;
                                break;
                            case "ecl":
                                p.EyeColor = value;
                                break;
                            case "pid":
                                p.PassportId = value;
                                break;
                            case "cid":
                                p.CountryId = value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return passports.Count(x => x.IsValid).ToString();
        }

        public string RunTwo()
        {
            List<Passport> passports = new List<Passport>();
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input04.txt").ToList();
            Passport p = new Passport();
            passports.Add(p);

            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i] == "")
                {
                    p = new Passport();
                    passports.Add(p);
                }
                else
                {
                    List<string> split = inputs[i].Split(' ').ToList();
                    for (int j = 0; j < split.Count; j++)
                    {
                        string key = split[j].Split(':')[0];
                        string value = split[j].Split(':')[1];
                        switch (key)
                        {
                            case "byr":
                                int test = int.Parse(value);
                                if (test >= 1920 && test <= 2002)
                                    p.BirthYear = test;
                                break;
                            case "iyr":
                                int test2 = int.Parse(value);
                                if(test2 >= 2010 && test2 <= 2020)
                                    p.IssueYear = test2;
                                break;
                            case "eyr":
                                int test3 = int.Parse(value);
                                if (test3 >= 2020 && test3 <= 2030)
                                    p.ExpirationYear = test3;
                                break;
                            case "hgt":
                                if (value.EndsWith("cm") && value.Length == 5)
                                {
                                    int test4 = int.Parse(value.Substring(0, 3));
                                    if (test4 >= 150 && test4 <= 193)
                                        p.Height = value;

                                }
                                else if (value.EndsWith("in"))
                                {
                                    int test4 = int.Parse(value.Substring(0, 2));
                                    if (test4 >= 59 && test4 <= 76)
                                        p.Height = value;
                                }
                                break;
                            case "hcl":
                                if (value.StartsWith("#") && value.Length == 7)
                                {
                                    Regex reg = new Regex((@"^[a-f0-9]+$"));

                                    if(reg.Match(value.Substring(1, 6)).Success)
                                        p.HairColor = value;
                                }
                                break;
                            case "ecl":
                                if(value == "amb" || value == "blu" || value == "brn" || value == "gry" || value == "grn" || value == "hzl" || value == "oth")
                                    p.EyeColor = value;
                                break;
                            case "pid":
                                if(value.Length == 9)
                                {
                                    int test5 = int.Parse(value);
                                    p.PassportId = value;
                                }
                                break;
                            case "cid":
                                p.CountryId = value;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return passports.Count(x => x.IsValid).ToString();
        }
    }
}