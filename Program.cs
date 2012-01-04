using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Globalization;
using System.Reflection;
using System.Net.Cache;

namespace PlayingWithCsharp
{
    public class location
    {
        string contact = "";
        string postalCode = "";
        string streetAddress = "";
        string city = "";
        string province = "";
        string country = "";
        string latitude = "";
        string longitude = "";
        string map = "";

        public string GetContact()
        {
            return (this.contact);
        }
        public void SetContact(string contact)
        {
            this.contact = contact;
        }

        public string GetPostalCode()
        {
            return (this.postalCode);
        }
        public void SetPostalCode(string PostalCode)
        {
            this.postalCode = PostalCode;
        }

        public string GetStreetAddress()
        {
            return (this.streetAddress);
        }
        public void SetStreetAddress(string streetAddress)
        {
            this.streetAddress = streetAddress;
        }

        public string GetCity()
        {
            return (this.city);
        }
        public void SetCity(string city)
        {
            this.city = city;
        }

        public string GetProvince()
        {
            return (this.province);
        }
        public void SetProvince(string province)
        {
            this.province = province;
        }

        public string GetCountry()
        {
            return (this.country);
        }
        public void SetCountry(string country)
        {
            this.country = country;
        }

        public string GetLatitude()
        {
            return (this.latitude);
        }
        public void SetLatitude(string latitude)
        {
            this.latitude = latitude;
        }

        public string GetLongitude()
        {
            return (this.longitude);
        }
        public void SetLongitude(string longitude)
        {
            this.longitude = longitude;
        }

        public string GetMap()
        {
            return (this.map);
        }
        public void SetMap(string map)
        {
            this.map = map;
        }

    }

    public class dealer
    {
        List<location> Locations = new List<location>();

        public void SetLocation(string streetAddress, string city, string province, string country, string postalCode, string contact, string latitude, string longitude, string map)
        {
            location newlocation = new location();
            newlocation.SetStreetAddress(streetAddress);
            newlocation.SetCity(city);
            newlocation.SetProvince(province);
            newlocation.SetCountry(country);
            newlocation.SetPostalCode(postalCode);
            newlocation.SetContact(contact);
            newlocation.SetLatitude(latitude);
            newlocation.SetLongitude(longitude);
            newlocation.SetMap(map);
            Locations.Add(newlocation);
        }
        public void SetLocation(location l)
        {
            Locations.Add(l);
        }
        public location GetLocation(int i)
        {
            return Locations[i];
        }
        public int CountLocations()
        {
            return (Locations.Count());
        }

    }

    public class deals
    {
        string dealID = "";
        string alternative = "";
        List<string> cities = new List<string>();

        public string GetDealID()
        {
            return (this.dealID);
        }
        public void SetDealID(string ID)
        {
            this.dealID = ID;
        }
        public string GetAlternativeID()
        {
            return (this.alternative);
        }
        public void SetAlternativeID(string ID)
        {
            this.alternative = ID;
        }
        public void AddCity(string city)
        {
            cities.Add(city);
        }
        public List<string> GetListCities()
        {
            return(this.cities);
        }
    }

    public class dealslist
    {
        List<deals> ListOfDeals = new List<deals>();

        public int DealEvaluated(string ID)
        {
            for (int i = 0; i < this.ListOfDeals.Count; i++)
            {
                if (this.ListOfDeals[i].GetAlternativeID() == ID)  // First this one. By default, alternativeID is "", so different from ID.
                    return (i);
                if (this.ListOfDeals[i].GetDealID() == ID)
                    return (i);
            }
            return (-1);
        }
        public void AddCity(int i, string city)
        {
            this.ListOfDeals[i].AddCity(city);
        }
        public void SetDealID(string ID, string alternative, string city)
        {
            deals newdeal = new deals();
            newdeal.SetDealID(ID);
            newdeal.SetAlternativeID(alternative);
            newdeal.AddCity(city);
            ListOfDeals.Add(newdeal);
        }
        public deals GetDealDetails(int i)
        {
            return ListOfDeals[i];
        }
        public int CountDeals()
        {
            return (ListOfDeals.Count());
        }
    }

    // Tags_attribtes data structure - This data structure contains, for each website, the tags that refer to the information we need to extract
    public class Tags
    {
        public string[] data = new string[50];
        public Tags()
        {
            for (int i = 0; i < 50; i++) data[i] = "";
        }

/*        public string Website="";
        public string InvalidLink = "";
        public string ListOfCities = "";
        public string DealID = "";
        public string DealLinkURL = "";
        public string Category = "";
        public string Company = "";
        public string CompanyURL = "";
        public string Image = "";
        public string Description = "";
        public string Lattitude = "";
        public string Longitude = "";
        public string CompleteAddress = "";
        public string StreetName = "";
        public string City = "";
        public string PostalCode = "";
        public string Country = "";
        public string Map = "";
        public string CompanyPhone = "";
        public string RegularPrice = "";
        public string OurPrice = "";
        public string Save = "";
        public string Discount = "";
        public string PayOutAmount = "";
        public string PayOutLink = "";
        public string SecondsTotal = "";
        public string SecondsElapsed = "";
        public string RemainingTime = "";
        public string ExpiryTime = "";
        public string MaxNumberOfVouchers = "";
        public string MinNunberOfVouchers = "";
        public string DealSoldOut = "";
        public string DealEnded = "";
        public string DealValid = "";
        public string PaidVoucherCount = "";
        public string Highlights = "";
        public string BuyDetails = "";
        public string DealText = "";
        public string Reviews = "";
        public string RelatedDeals = "";
        public string SideDeals = ""; */
    }

    public class keywords
    {
        int times = 0;
        string keyword = "";
        char type = ' ';
        char direction = '>';
        int index = -1; // if keyword contais a reference to a DealData data ($), index will contain the DealData position of that data. 
        public int GetTimes()
        {
            return (this.times);
        }
        public void SetTimes(int i)
        {
            this.times = i;
        }
        public int GetIndex()
        {
            return (this.index);
        }
        public void SetIndex(int i)
        {
            this.index = i;
        }
        public string GetKeyword()
        {
            return (this.keyword);
        }
        public void SetKeyword(string k)
        {
            this.keyword = k;
        }
        public char GetType()
        {
            return (this.type);
        }
        public void SetType(char c)
        {
            this.type = c;
        }
        public char GetDirection()
        {
            return (this.direction);
        }
        public void SetDirection(char c)
        {
            this.direction = c;
        }
    }

    public class MainOperations
    {

/*        public static keywords GetEndString(string str, ref int c1, ref string read, int read_pos)
        {
            keywords k = new keywords();
            if (c1 > str.Length)
            {
                Console.WriteLine("Mistake at the end of End keyword (@).");
                k.SetTimes(-1);
                return (k);
            }
            while (str[c1] == ' ')
            {
                c1 += 1;
            }
            if (str[c1] == '<')
            {
                Console.WriteLine("ERROR: End delimiter can't search back.");
                k.SetTimes(-1);
                return (k);
            }
            if ((str[c1] >= '1') && (str[c1] <= '9'))
            {
                do
                {
                    k.SetTimes(10 * k.GetTimes() + str[c1]);
                    c1 += 1;
                } while ((str[c1] >= '1') && (str[c1] <= '9'));
                while (str[c1] == ' ')
                {
                    c1 += 1;
                }
            }
            if (str[c1] == '"')
            {
                int c2 = c1;
                c1 += 1;
                do
                {
                    c2 = str.IndexOf('"', c2 + 1);
                    if (c2 == -1)
                    {
                        Console.WriteLine("Missing \" in End keyword. Can't go on.");
                        k.SetTimes(-1);
                        return (k);
                    }
                } while (str[c2 - 1] == '\\');
                k.SetKeyword(str.Substring(c1, c2-c1));
                k.SetKeyword(k.GetKeyword().Replace("\\\"","\""));
                if (k.GetTimes() == 0) k.SetTimes(1);
                k.SetType('@');
                c1 = c2 + 1;
            }
            else
            {
                Console.WriteLine("Error in End tag (@) format. Probably missing \" at " + c1 + " character.");
                k.SetTimes(-1);
            }
            if ((c1 < str.Length) && (str[c1] == '-'))
            {
                c1 += 1;
                string tillHere = MainOperations.GetConstant(str, ref c1);
                int aux_pos = read.IndexOf(tillHere, read_pos);
                if (aux_pos != -1)
                {
                    read = read.Substring(0, aux_pos);
                }
            }
            return (k);
        }
        */
    }
    
    public class Extraction
    {
        Tags oneWebsite;
        string baseAddress;
        List<string> DontHandleFirstPage;
        List<int> RecursList = new List<int>();
        string AtTheEnd;

        public Extraction(Tags oneWebsite, string baseAddress, List<string> DontHandleFirstPage)
        {
            this.oneWebsite = oneWebsite;
            this.baseAddress = baseAddress;
            this.DontHandleFirstPage = DontHandleFirstPage;
        }

        public keywords GetSearchString(string str, ref int c1, Tags DealData, string read)
        {
            keywords k = new keywords();
            if (c1 > str.Length)
            {
                Console.WriteLine("Mistake at the end of Search keyword (?).");
                AtTheEnd += "ERROR: Mistake at the end of Search keyword (?).";
                k.SetTimes(-1);
                return (k);
            }
            while (str[c1] == ' ')
            {
                c1 += 1;
            }
            if (str[c1] == '<')
            {
                k.SetDirection('<');
                c1 += 1;
                while (str[c1] == ' ')
                {
                    c1 += 1;
                }
            }
            if ((str[c1] >= '1') && (str[c1] <= '9'))
            {
                do
                {
                    k.SetTimes(10 * k.GetTimes() + (str[c1] - 48));
                    c1 += 1;
                } while ((str[c1] >= '1') && (str[c1] <= '9'));
                while (str[c1] == ' ')
                {
                    c1 += 1;
                }
            }
            if (str[c1] == '$')
            {
                int c2;
                c1 += 1;
                c2 = c1 + 1;
                int num = -1;
                if ((str[c2] >= '0') && (str[c2] <= '9') && (str[c1] >= '0') && (str[c1] <= '9'))
                {
                    num = Convert.ToInt16(str.Substring(c1, 2));
                    c1 += 1;
                }
                else if ((str[c1] >= '0') && (str[c1] <= '9'))
                {
                    num = Convert.ToInt16(str.Substring(c1, 1));
                }
                else
                {
                    Console.WriteLine("Mistake at variable ($).");
                    AtTheEnd += "ERROR: Mistake at variable ($).";
                    k.SetTimes(-1);
                    return (k);
                }
                if (DealData.data[num] == "")
                {
                    str = this.oneWebsite.data[num];
                    if (!RecursList.Contains(num))
                    {
                        RecursList.Add(num);
                        str = this.SingleDataExtraction(str, read, DealData);
                        RecursList.Remove(num);
                        DealData.data[num] = str;
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Recursivity definition!" + str);
                        AtTheEnd += "ERROR: Recursivity definition!" + str;
                        k.SetTimes(-1);
                        return (k);
                    }
                }
                k.SetKeyword(DealData.data[num]);
                k.SetIndex(num);
                if (k.GetTimes() == 0) k.SetTimes(1);
                k.SetType('?');
                c1 += 1;
            }
            else if (str[c1] == '"')
            {
                int c2 = c1;
                c1 += 1;
                do
                {
                    c2 = str.IndexOf('"', c2 + 1);
                    if (c2 == -1)
                    {
                        Console.WriteLine("Missing \" in Search keyword. Can't go on.");
                        AtTheEnd += "ERROR: Missing \" in Search keyword. Can't go on.";
                        k.SetTimes(-1);
                        return (k);
                    }
                } while (str[c2 - 1] == '\\');
                k.SetKeyword(str.Substring(c1, c2 - c1));
                k.SetKeyword(k.GetKeyword().Replace("\\\"", "\""));
                if (k.GetTimes() == 0) k.SetTimes(1);
                k.SetType('?');
                c1 = c2 + 1;
            }
            else
            {
                Console.WriteLine("Error in Search tag (?) format. Probably missing \" at " + c1 + " character.");
                AtTheEnd += "ERROR: Error in Search tag (?) format. Probably missing \" at " + c1 + " character.";
                k.SetTimes(-1);
            }
            return (k);
        }

        public keywords GetEndString(string str, ref int c1)
        {
            keywords k = new keywords();
            if (c1 > str.Length)
            {
                Console.WriteLine("Mistake at the end of End keyword (@).");
                AtTheEnd += "ERROR: Mistake at the end of End keyword (@).";
                k.SetTimes(-1);
                return (k);
            }
            while (str[c1] == ' ')
            {
                c1 += 1;
            }
            if (str[c1] == '<')
            {
                Console.WriteLine("ERROR: End delimiter can't search back.");
                AtTheEnd += "ERROR: End delimiter can't search back.";
                k.SetTimes(-1);
                return (k);
            }
            if ((str[c1] >= '1') && (str[c1] <= '9'))
            {
                do
                {
                    k.SetTimes(10 * k.GetTimes() + str[c1]);
                    c1 += 1;
                } while ((str[c1] >= '1') && (str[c1] <= '9'));
                while (str[c1] == ' ')
                {
                    c1 += 1;
                }
            }
            if (str[c1] == '"')
            {
                int c2 = c1;
                c1 += 1;
                do
                {
                    c2 = str.IndexOf('"', c2 + 1);
                    if (c2 == -1)
                    {
                        Console.WriteLine("Missing \" in End keyword. Can't go on.");
                        AtTheEnd += "ERROR: Missing \" in End keyword. Can't go on.";
                        k.SetTimes(-1);
                        return (k);
                    }
                } while (str[c2 - 1] == '\\');
                k.SetKeyword(str.Substring(c1, c2 - c1));
                k.SetKeyword(k.GetKeyword().Replace("\\\"", "\""));
                if (k.GetTimes() == 0) k.SetTimes(1);
                k.SetType('@');
                c1 = c2 + 1;
            }
            else
            {
                Console.WriteLine("Error in End tag (@) format. Probably missing \" at " + c1 + " character.");
                AtTheEnd += "ERROR: Error in End tag (@) format. Probably missing \" at " + c1 + " character.";
                k.SetTimes(-1);
            }
            return (k);
        }

        public string GetConstant(string str, ref int c1)
        {
            string str_new = "";
            if (c1 > str.Length)
            {
                Console.WriteLine("Mistake at the end of #constant/@- keyword.");
                AtTheEnd += "ERROR: Mistake at the end of #constant/@- keyword.";
                return ("{:-(");
            }
            while (str[c1] == ' ')
            {
                c1 += 1;
            }
            if (str[c1] == '"')
            {
                int c2 = c1;
                c1 += 1;
                do
                {
                    c2 = str.IndexOf('"', c2 + 1);
                    if (c2 == -1)
                    {
                        Console.WriteLine("Missing \" in #Constant/@- keyword. Can't go on.");
                        AtTheEnd += "ERROR: Missing \" in #Constant/@- keyword. Can't go on.";
                        return ("{:-(");
                    }
                } while (str[c2 - 1] == '\\');
                str_new = str.Substring(c1, c2 - c1);
                c1 = c2 + 1;
            }
            else
            {
                Console.WriteLine("Error in #Constant tag format. Probably missing \" at " + c1 + " character.");
                AtTheEnd += "ERROR: Error in #Constant tag format. Probably missing \" at " + c1 + " character.";
                return ("{:-(");
            }
            str_new = str_new.Replace("\\|","|");
            return (str_new);
        }

        public string GetRepeatedOperation(string str, ref int c1, ref string read)
        {
            string str_new = "";
            if (c1 > str.Length)
            {
                Console.WriteLine("Mistake at the end of ( keyword.");
                AtTheEnd += "ERROR: Mistake at the end of ( keyword.";
                return ("{:-(");
            }
            int c2 = c1 - 1;
            do
            {
                c2 = str.IndexOf(')', c2 + 1);
                if (c2 == -1)
                {
                    Console.WriteLine("Missing ). Can't go on.");
                    AtTheEnd += "ERROR: Missing ). Can't go on.";
                    return ("{:-(");
                }
            } while (str[c2 - 1] == '\\');
            str_new = str.Substring(c1, c2 - c1);
            c2 += 1;

            if ((c2 < str.Length) && (str[c2] == '-'))
            {
                c2 += 1;
                keywords aux = GetEndString(str, ref c2);
                int aux_pos = 0;
                while (aux.GetTimes() > 0)
                {
                    aux.SetTimes(aux.GetTimes() - 1);
                    aux_pos = read.IndexOf(aux.GetKeyword(), aux_pos);
                    //                    if (aux_pos == -1)
                    //                    {
                    //                        Console.WriteLine("The data you are looking for can not be found in the HTML file");
                    //                        return("{:-(");
                    //                    }
                    if (aux_pos > 0)
                        aux_pos += aux.GetKeyword().Length;
                }
                if (aux_pos != -1)
                {
                    aux_pos -= aux.GetKeyword().Length;
                    read = read.Substring(0, aux_pos);
                }
            }
            c1 = c2;
            return (str_new);
        }

        public string SingleDataExtraction(string str, string read)
        {
            int aux = 0;
            Tags DealData = new Tags();
            return (SingleDataExtraction(str, read, DealData, ref aux));
        }

        public string SingleDataExtraction(string str, string read, Tags DealData)
        {
            int aux = 0;
            return (SingleDataExtraction(str, read, DealData, ref aux));
        }

        public string SingleDataExtraction(string str, string read, Tags DealData, ref int position)
        {
            int read_pos = position;
            int pos_ini = position;
            string result = "";
            keywords search = new keywords();
            keywords end = new keywords();
            bool op_rep = false;
            int c = 0;
            int temp_c = 0;
            string temp_str = "";
            string temp_result = "";
            string err_not_found = "";
            string temp_read = read;
            string separator = "\n";
            string constValue = "";
            while ((c < str.Length) || (op_rep))
            {
                if ((op_rep) && (c >= str.Length))
                {
                    if (read_pos != -1)
                    {
                        c = 0;
                        RemoveTags(ref result);
                        RemoveAmpersand(ref result);
                        temp_result = temp_result + result + separator;
                        result = "";
                    }
                    else
                    {
                        op_rep = false;
                        c = temp_c;
                        str = temp_str;
                        RemoveTags(ref result);
                        RemoveAmpersand(ref result);
                        search = new keywords();
                        end = new keywords();
                        if (result != "")
                        {
                            temp_result = temp_result + result;
                        }
                        result = temp_result;
//                        return (temp_result);
                    }
                }

                else if (str[c] == '|')
                {
                    if (((c + 1) < str.Length) && (str[c + 1] == '|') && (!op_rep))
                    {
                        if (err_not_found != "")
                            result = "";
                        else
                        {
                            RemoveTags(ref result);
                            if (result == "http://")
                                result = "";
                            if (result != "")
                            {
                                c = str.Length;
                                continue;
                            }
                        }
                        read_pos = pos_ini;
                        read = temp_read;
                        err_not_found = "";
                        c += 2;
                        search = new keywords();
                        end = new keywords();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Mistake at the Tag/Keyword (|).");
                        AtTheEnd += "ERROR: Mistake at the Tag/Keyword (|).";
                        return ("{:-(");
                    }
                }

                // getting the operations that must be executed continuasly till the end of the html page
                else if (str[c] == '(')
                {
                    string r;
                    c += 1;
                    r = GetRepeatedOperation(str, ref c, ref read);
                    op_rep = true;
                    if ((c < str.Length) && (str[c] == ','))
                    {
                        c += 1;
                        separator = GetConstant(str, ref c);
                    }
                    // store the values of str and c to handle it after finishing the repeated operation
                    temp_str = str;
                    temp_c = c;
                    temp_result = result;
                    str = r;
                    c = 0;
                    result = "";
                    continue;
                }

                // removing spaces and delimiters (like ;)
                else if ((str[c] == ' ') || (str[c] == ';'))
                {
                    c += 1;
                    continue;
                }

                else if (str[c] == '$')
                {
                    int c2;
                    c = c + 1;
                    c2 = c + 1;
                    int num = -1;
                    if ((str[c2] >= '0') && (str[c2] <= '9') && (str[c] >= '0') && (str[c] <= '9'))
                    {
                        num = Convert.ToInt16(str.Substring(c, 2));
                        c += 1;
                    }
                    else if ((str[c] >= '0') && (str[c] <= '9'))
                    {
                        num = Convert.ToInt16(str.Substring(c, 1));
                    }
                    else
                    {
                        Console.WriteLine("Mistake at variable ($): Missing number.");
                        AtTheEnd += "ERROR: Mistake at variable ($): Missing number.";
                        if ((c < str.Length) && (str[c] != '|'))
                        {
                            do
                            {
                                c = str.IndexOf('|', c + 1);
                            } while ((c != -1) && (str[c - 1] == '\\'));
                        }
                        if (c == -1)
                            c = str.Length;
                        err_not_found = "Mistake at variable ($): Missing number.";
                        continue;
                    }
                    if (DealData.data[num] == "")
                    {
                        string str2 = this.oneWebsite.data[num];
                        if (!RecursList.Contains(num))
                        {
                            RecursList.Add(num);
                            str2 = this.SingleDataExtraction(str2, read, DealData);
                            RecursList.Remove(num);
                            DealData.data[num] = str2;
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Recursivity definition!" + str2);
                            AtTheEnd += "ERROR: Recursivity definition!" + str2;
                            if ((c < str.Length) && (str[c] != '|'))
                            {
                                do
                                {
                                    c = str.IndexOf('|', c + 1);
                                } while ((c != -1) && (str[c - 1] == '\\'));
                            }
                            if (c == -1)
                                c = str.Length;
                            err_not_found = "ERROR: Recursivity definition!";
                            continue;
                        }
                    }
                    result = result + DealData.data[num];
                    c += 1;
                }


                // reseting source page position.
                else if (str[c] == '0')
                {
                    read_pos = pos_ini;
                    read = temp_read;
                    c += 1;
                    continue;
                }
                // getting the constant string that must be added to the information we are looking for
                else if (str[c] == '#')
                {
                    string r;
                    c += 1;
                    r = GetConstant(str, ref c);
                    if (r == "{:-(")
                    {
                        Console.WriteLine("Constant (#) not found.");
                        AtTheEnd += "Constant (#) not found.";
                        if ((c < str.Length) && (str[c] != '|'))
                        {
                            do
                            {
                                c = str.IndexOf('|', c + 1);
                            } while ((c != -1) && (str[c - 1] == '\\'));
                        }
                        if (c == -1)
                            c = str.Length;
                        err_not_found = "Constant (#) not found.";
                        continue;
                    }
                    result = result + r;
                    continue;
                }
                // getting string that is located before the information we need on html page
                else if (str[c] == '?')
                {
                    while (search.GetTimes() > 0)
                    {
                        int saved_pos = read_pos;
                        // search for the current keyword on html page. Variable search (Keywords) will be overwritten 
                        if (search.GetDirection() == '>')
                        {
                            int optional = 0;
                            if (read_pos == -1)
                                break;
                            read_pos = read.IndexOf(search.GetKeyword(), read_pos);
                            if (read_pos == -1) // some cases, there are more than one value to be searched (when $ is used) and the || is the separator of those values
                            {
                                int begin_opt = 0;
                                optional = search.GetKeyword().IndexOf("||");
                                while (optional != -1)
                                {
                                    read_pos = read.IndexOf(search.GetKeyword().Substring(begin_opt, optional - begin_opt), saved_pos);
                                    if (read_pos != -1)
                                    {
                                        if (begin_opt != 0)
                                        {
                                            string k = search.GetKeyword();
                                            string s = k.Substring(begin_opt, optional - begin_opt);
                                            k = k.Replace("||" + s, "");
                                            k = s + "||" + k;   // put the optional keyword at the front, because this is the one that was found on HTML page and this is the correct search one. Only the front one will be kept later. 
                                            if (search.GetIndex() != -1)
                                                DealData.data[search.GetIndex()] = k;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        if (optional >= search.GetKeyword().Length)
                                            optional = -1;
                                        else
                                        {
                                            begin_opt = optional + 2;
                                            optional = search.GetKeyword().IndexOf("||", begin_opt);
                                            if ((optional == -1) && (begin_opt < search.GetKeyword().Length)) // this is the last element, that is why optionao is not -1 now
                                                optional = search.GetKeyword().Length;
                                        }
                                    }
                                }
                            }
                            if (read_pos == -1)
                            {
                                do
                                {
                                    c = str.IndexOf('|', c + 1);
                                } while ((c != -1) && (str[c - 1] == '\\'));
                                position = read_pos;
                                if (c == -1)
                                    c = str.Length;
                                if (op_rep)
                                {
                                    result = "";
                                    break;
                                }
                                err_not_found = "The data you are looking for can not be found in the HTML file";
                                //                                Console.WriteLine("The data you are looking for can not be found in the HTML file");
                                break;
                                //                                return ("{:-("); //ends the Thread?
                            }
                            read_pos += search.GetKeyword().Length;
                        }
                        else
                        {
                            int begin_opt = 0;
                            int optional = 0;
                            if (read_pos == -1)
                                break;
                            read_pos = read.LastIndexOf(search.GetKeyword(), read_pos);
                            if (read_pos == -1) // some cases, there are more than one value to be searched and the || is the separator of those values
                            {
                                optional = search.GetKeyword().IndexOf("||");
                                while (optional != -1)
                                {
                                    read_pos = read.LastIndexOf(search.GetKeyword().Substring(begin_opt, optional - begin_opt), saved_pos);
                                    if (read_pos != -1)
                                    {
                                        if (begin_opt != 0)
                                        {
                                            string k = search.GetKeyword();
                                            string s = k.Substring(begin_opt, optional - begin_opt);
                                            k = k.Replace("||" + s, "");
                                            k = s + "||" + k;   // put the optional keyword at the front, because this is the one that was found on HTML page and this is the correct search one. Only the front one will be kept later. 
                                            if (search.GetIndex() != -1)
                                                DealData.data[search.GetIndex()] = k;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        if (optional >= search.GetKeyword().Length)
                                            optional = -1;
                                        else
                                        {
                                            begin_opt = optional + 2;
                                            optional = search.GetKeyword().IndexOf("||", begin_opt);
                                            if ((optional == -1) && (begin_opt < search.GetKeyword().Length))
                                                optional = search.GetKeyword().Length;
                                        }
                                    }
                                }
                            }
                            if (read_pos == -1)
                            {
                                do
                                {
                                    c = str.IndexOf('|', c + 1);
                                } while ((c != -1) && (str[c - 1] == '\\'));
                                position = read_pos;
                                if (c == -1)
                                    c = str.Length;
                                err_not_found = "The data you are looking for can not be found in the HTML file";
                                //                               Console.WriteLine("The data you are looking for can not be found in the HTML file");
                                //                            c += 1;
                                break;
                                //                                return("{:-("); //ends the Thread?
                            }
                            else
                                if (search.GetTimes() > 1)
                                    read_pos -= 1;
                                else
                                    read_pos += search.GetKeyword().Length;
                        }
                        byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(search.GetKeyword());
                        string normal = System.Text.Encoding.UTF8.GetString(tempBytes);
                        if ((normal == search.GetKeyword()) || (read_pos != -1))
                            search.SetTimes(search.GetTimes() - 1);
                        else
                        {
                            read_pos = saved_pos;
                            search.SetKeyword(normal);
                        }
                    }
                    if ((c >= str.Length) || (read_pos == -1))
                        continue;
                    c += 1;
                    search = new keywords();
                    search = GetSearchString(str, ref c, DealData, read);
//                    if (search.GetTimes() == -1)
//                    {
//                        return; // ends the Thread?
//                    }

                    if ((c < str.Length) && (str[c] == '-'))
                    {
                        c += 1;
                        string tillHere = GetConstant(str, ref c);
                        if (read_pos != -1)
                        {
                            int aux_pos = read.IndexOf(tillHere, read_pos);
                            if (aux_pos != -1)
                            {
                                aux_pos += tillHere.Length;
                                read = read.Substring(0, aux_pos);
                            }
                        }
                    }
                    
                    continue;
                }

                // getting string that is located after the information we need on html page
                else if (str[c] == '@')
                {
                    int end_pos;
                    c += 1;
                    //                    if (end.GetTimes() == -1)
                    //                    {
                    //                        return; // ends the Thread?
                    //                    }

                    // Start extracting cities
                    end = GetEndString(str, ref c);
                    while (search.GetTimes() > 0)
                    {
                        int saved_pos = read_pos;
                        // search for the current keyword on html page. Variable search (Keywords) will be overwritten 
                        if (search.GetDirection() == '>')
                        {
                            int begin_opt = 0;
                            int optional = 0;
                            if (read_pos == -1)
                                break;
                            read_pos = read.IndexOf(search.GetKeyword(), read_pos);
                            if (read_pos == -1) // some cases, there are more than one value to be searched and the || is the separator of those values
                            {
                                optional = search.GetKeyword().IndexOf("||");
                                while (optional != -1)
                                {
                                    read_pos = read.IndexOf(search.GetKeyword().Substring(begin_opt, optional - begin_opt), saved_pos);
                                    if (read_pos != -1)
                                    {
                                        if (begin_opt != 0)
                                        {
                                            string k = search.GetKeyword();
                                            string s = k.Substring(begin_opt, optional - begin_opt);
                                            k = k.Replace("||" + s, "");
                                            k = s + "||" + k;   // put the optional keyword at the front, because this is the one that was found on HTML page and this is the correct search one. Only the front one will be kept later. 
                                            if (search.GetIndex() != -1)
                                                DealData.data[search.GetIndex()] = k;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        if (optional >= search.GetKeyword().Length)
                                            optional = -1;
                                        else
                                        {
                                            begin_opt = optional + 2;
                                            optional = search.GetKeyword().IndexOf("||", begin_opt);
                                            if ((optional == -1) && (begin_opt < search.GetKeyword().Length))
                                                optional = search.GetKeyword().Length;
                                        }
                                    }
                                }
                            }
                            if (read_pos == -1)
                            {
                                if ((c < str.Length) && (str[c] != '|'))
                                {
                                    do
                                    {
                                        if (c + 1 < str.Length)
                                        {
                                            c = str.IndexOf('|', c + 1);
                                        }
                                        else
                                            c = -1;
                                    } while ((c != -1) && (str[c - 1] == '\\'));
                                }
                                position = read_pos;
                                if (c == -1)
                                    c = str.Length;
                                if (op_rep)
                                {
                                    result = "";
                                    break;
                                }
                                err_not_found = "The data you are looking for can not be found in the HTML file";
                                //                               Console.WriteLine("The data you are looking for can not be found in the HTML file");
                                break;
                                //                                return("{:-("); //ends the Thread?
                            }
                            read_pos += search.GetKeyword().Length;
                        }
                        else
                        {
                            int begin_opt = 0;
                            int optional = 0;
                            if (read_pos == -1)
                                break;
                            read_pos = read.LastIndexOf(search.GetKeyword(), read_pos);
                            if (read_pos == -1) // some cases, there are more than one value to be searched and the || is the separator of those values
                            {
                                optional = search.GetKeyword().IndexOf("||");
                                while (optional != -1)
                                {
                                    read_pos = read.LastIndexOf(search.GetKeyword().Substring(begin_opt, optional - begin_opt), saved_pos);
                                    if (read_pos != -1)
                                    {
                                        if (begin_opt != 0)
                                        {
                                            string k = search.GetKeyword();
                                            string s = k.Substring(begin_opt, optional - begin_opt);
                                            k = k.Replace("||" + s, "");
                                            k = s + "||" + k;   // put the optional keyword at the front, because this is the one that was found on HTML page and this is the correct search one. Only the front one will be kept later. 
                                            if (search.GetIndex() != -1)
                                                DealData.data[search.GetIndex()] = k;
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        if (optional >= search.GetKeyword().Length)
                                            optional = -1;
                                        else
                                        {
                                            begin_opt = optional + 2;
                                            optional = search.GetKeyword().IndexOf("||", begin_opt);
                                            if ((optional == -1) && (begin_opt < search.GetKeyword().Length))
                                                optional = search.GetKeyword().Length;
                                        }
                                    }
                                }
                            }
                            if (read_pos == -1)
                            {
                                if ((c < str.Length) && (str[c] != '|'))
                                {
                                    do
                                    {
                                        if (c + 1 < str.Length)
                                        {
                                            c = str.IndexOf('|', c + 1);
                                        }
                                        else
                                            c = -1;
                                    } while ((c != -1) && (str[c - 1] == '\\'));
                                }
                                position = read_pos;
                                if (c == -1)
                                    c = str.Length;
                                err_not_found = "The data you are looking for can not be found in the HTML file";
                                //                              Console.WriteLine("The data you are looking for can not be found in the HTML file");
                                break;
                                //                                return("{:-("); //ends the Thread?
                            }
                            else
                                if (search.GetTimes() > 1)
                                    read_pos -= 1;
                                else
                                    read_pos += search.GetKeyword().Length;
                        }
                        byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(search.GetKeyword());
                        string normal = System.Text.Encoding.UTF8.GetString(tempBytes);
                        if ((normal == search.GetKeyword()) || (read_pos != -1))
                            search.SetTimes(search.GetTimes() - 1);
                        else
                        {
                            read_pos = saved_pos;
                            search.SetKeyword(normal);
                        }
                    }

                    if ((op_rep) && (read_pos == -1))
                    {
                        continue;
                    }

                    if (read_pos == -1)
                    {
                        continue;
                    }

                    if ((c < str.Length) && (str[c] == '-'))
                    {
                        c += 1;
                        string tillHere = GetConstant(str, ref c);
                        if (read_pos != -1)
                        {
                            int aux_pos = read.IndexOf(tillHere, read_pos);
                            if (aux_pos != -1)
                            {
                                aux_pos += tillHere.Length;
                                read = read.Substring(0, aux_pos);
                            }
                        }
                    }

                    end_pos = read_pos;

                    while (end.GetTimes() > 0)
                    {
                        end.SetTimes(end.GetTimes() - 1);
                        // search for the current keyword on html page. Variable search (Keywords) will be overwritten 
                        if (end_pos == -1)
                            break;
                        end_pos = read.IndexOf(end.GetKeyword(), end_pos);
                        position = end_pos;
                        if (end_pos == -1)
                        {
                            if ((c < str.Length) && (str[c] != '|'))
                            {
                                do
                                {
                                    c = str.IndexOf('|', c + 1);
                                } while ((c != -1) && (str[c - 1] == '\\'));
                            }
                            if (c == -1)
                                c = str.Length;
                            err_not_found = "The data you are looking for can not be found in the HTML file";
 //                           Console.WriteLine("The data you are looking for can not be found in the HTML file");
                            continue;
                            //                            return("{:-("); //ends the Thread?
                        }
                        end_pos += end.GetKeyword().Length;
                    }
                    if (end_pos != -1)
                    {
                        end_pos -= end.GetKeyword().Length;

                        result = result + read.Substring(read_pos, end_pos - read_pos);
                        read_pos = end_pos;
                    }
                    position = end_pos;
                    search = new keywords();
                    end = new keywords();
                }
                else
                {
                    Console.WriteLine("Mistake: Unrecognized " + str[c] + " at Tag/Keyword");
                    AtTheEnd += "ERROR: Mistake: Unrecognized " + str[c] + " at Tag/Keyword";
                    return ("{:-(");
                }
            }
            if (err_not_found != "")
                return("{:-(");
            RemoveTags(ref result);
            if (result == "http://")
                result = "";
            RemoveAmpersand(ref result);
            return (result);
        }

        private void RemoveTags(ref string result)
        {
            result = result.Replace("<br />", "\n");
            result = result.Replace("<br/>", "\n");
            result = result.Replace("</br>", "\n");
            result = result.Replace("<p>", "\n");
            result = result.Replace("</p>", "\n\n");
            result = result.Replace("</li>", "\n");
            result = result.Replace("</td>", "; ");
            result = result.Replace("<i>", "");
            result = result.Replace("</i>", "");
            result = result.Replace("<tr>", "");
            result = result.Replace("</tr>", "\n");
            result = result.Replace("<strong>", "");
            result = result.Replace("</strong>", "");
            result = result.Replace("</div>", "");
            result = result.Replace("</font>", "");
            result = result.Replace("</blockquote>", "");
            result = result.Replace("</span>", "");
            result = result.Replace("</a>", "");
            result = result.Replace("<b>", "\n");
            result = result.Replace("</b>", "\n\n");
            result = result.Replace("<em>", "\n");
            result = result.Replace("</em>", "\n\n");
            result = result.Replace("%20", " ");

            string htmlTag = "<td";
            int i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i+1);
                if (j != -1)
                    result = result.Replace(result.Substring(i, j - i + 1), "");
                else
                    result = result.Replace(result.Substring(i, result.Length - i), "");
                i = result.IndexOf(htmlTag);
            }
            htmlTag = "<div";
            i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i + 1);
                if (j != -1)
                    result = result.Replace(result.Substring(i, j - i + 1), "");
                else
                    result = result.Replace(result.Substring(i, result.Length - i), "");
                i = result.IndexOf(htmlTag);
            }
            htmlTag = "<p";
            i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i + 1);
                if (j != -1)
                    result = result.Replace(result.Substring(i, j - i + 1), "");
                else
                    result = result.Replace(result.Substring(i, result.Length - i), "");
                i = result.IndexOf(htmlTag);
            }
            htmlTag = "<span";
            i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i + 1);
                if (j != -1)
                    result = result.Replace(result.Substring(i, j - i + 1), "");
                else
                    result = result.Replace(result.Substring(i, result.Length - i), "");
                i = result.IndexOf(htmlTag);
            }
            htmlTag = "<a";
            i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i + 1);
                if (j != -1)
                    result = result.Replace(result.Substring(i, j - i + 1), "");
                else
                    result = result.Replace(result.Substring(i, result.Length - i), "");
                i = result.IndexOf(htmlTag);
            }
            htmlTag = "<br";
            i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i + 1);
                if (j != -1)
                    result = result.Replace(result.Substring(i, j - i + 1), "");
                else
                    result = result.Replace(result.Substring(i, result.Length - i), "");
                i = result.IndexOf(htmlTag);
            }
            htmlTag = "<!--";
            i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i + 1);
                if (j != -1)
                    result = result.Replace(result.Substring(i, j - i + 1), "");
                else
                    result = result.Replace(result.Substring(i, result.Length - i), "");
                i = result.IndexOf(htmlTag);
            }
            htmlTag = "<strong";
            i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i + 1);
                if (j != -1)
                    result = result.Replace(result.Substring(i, j - i + 1), "");
                else
                    result = result.Replace(result.Substring(i, result.Length - i), "");
                i = result.IndexOf(htmlTag);
            }
            htmlTag = "<blockquote";
            i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i + 1);
                if (j != -1)
                    result = result.Replace(result.Substring(i, j - i + 1), "");
                else
                    result = result.Replace(result.Substring(i, result.Length - i), "");
                i = result.IndexOf(htmlTag);
            }
            htmlTag = "<font";
            i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i + 1);
                if (j != -1)
                    result = result.Replace(result.Substring(i, j - i + 1), "");
                else
                    result = result.Replace(result.Substring(i, result.Length - i), "");
                i = result.IndexOf(htmlTag);
            }

            htmlTag = "<li";
            i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i + 1);
                if (j != -1)
                {
                    while ((j < result.Length) && ((result[j] == ' ') || (result[j] == '\t')))
                        j++;
                    if (j > i + 3)
                        result = result.Replace(result.Substring(i + 3, j - i - 3), "");
                    i = result.IndexOf(htmlTag, i + 1);
                }
                else
                {
                    result = result.Replace(result.Substring(i, result.Length - i), "");
                    i = result.IndexOf(htmlTag, i + 1);
                }
            }

            RemoveSpaces(ref result, false);
        }

        private void RemoveAmpersand(ref string result)
        {
            result = result.Replace("&amp;", "&");
            result = result.Replace("&#039;", "'");
            result = result.Replace("&rsquo;", "’");
            result = result.Replace("&nbsp;", "");
            result = result.Replace("&quot;", "\"");
            result = result.Replace("&lt;", "<");
            result = result.Replace("&gt;", ">");
            result = result.Replace("&tilde;", "~");
            RemoveSpaces(ref result, false);
        }

        
        private void RemoveSpaces(ref string temp_result, Boolean ponctuation) // Ponctuation indicates if it has to remove ponctuation characters. It must be true only when handling the data and false when extracting from webpages
        {
            int b = 0;
            int e = temp_result.Length - 1;
            if ((e == 0) && (temp_result[0] != ' ') && (temp_result[0] != '\n') && (temp_result[0] != '\t'))
                return;
            if (ponctuation)
            {
                while ((b <= e) && ((temp_result[b] == ' ') || (temp_result[b] == 160) || (temp_result[b] == '\n') || (temp_result[b] == '\t') || (temp_result[b] == '|') || (isPunctuation(temp_result[b]))))
                {
                    b += 1;
                }
                while ((e > b) && ((temp_result[e] == ' ') || (temp_result[e] == 160) || (temp_result[e] == '\n') || (temp_result[e] == '\t') || (temp_result[b] == '|') || (isPunctuation(temp_result[e]))))
                {
                    e -= 1;
                }
            }
            else
            {
                while ((b <= e) && ((temp_result[b] == ' ') || (temp_result[b] == 160) || (temp_result[b] == '\n') || (temp_result[b] == '\t') || (temp_result[b] == '|')))
                {
                    b += 1;
                }
                while ((e > b) && ((temp_result[e] == ' ') || (temp_result[e] == 160) || (temp_result[e] == '\n') || (temp_result[e] == '\t') || (temp_result[b] == '|')))
                {
                    e -= 1;
                }
            }
            if (e >= b)
                temp_result = temp_result.Substring(b, (e + 1) - b);
            else
                temp_result = "";
        }

        private bool isPunctuation(char p)
        {
            if ((p == ',') || (p == '.') || (p == ';') || (p == ':') || (p == '?') || (p == '!'))
                return true;
            return false;
        }

        string DownloadData(string URL)
        {
            // Set a default policy level for the "http:" and "https" schemes.
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.Default);
            HttpWebRequest.DefaultCachePolicy = policy;
            // Create the request.
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            // Define a cache policy for this request only. 
            HttpRequestCachePolicy noCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            req.CachePolicy = noCachePolicy;
            System.Text.Encoding encode = System.Text.Encoding.GetEncoding("ISO-8859-1");
            StreamReader sr = default(StreamReader);
            string strReturn = null;
            req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.11) Gecko/20071127 Firefox/2.0.0.11'";
            req.Timeout = 180000;
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                sr = new StreamReader(resp.GetResponseStream(), System.Text.Encoding.GetEncoding(resp.CharacterSet));
                strReturn = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: Exception reading from webpage " + URL);
                strReturn = "ERROR: Exception reading from webpage";
            }
            return strReturn;

/*            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
//            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.12) Gecko/20101026 Firefox/3";
            request.Accept = "Accept: text/html,application/xhtml+xml,application/xml";
            string strData = "";
            try
            {
   //             request.Proxy = WebProxy.GetDefaultProxy();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                System.IO.Stream stream = response.GetResponseStream();
                System.Text.Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                System.IO.StreamReader reader = new System.IO.StreamReader(stream, ec);
                strData = reader.ReadToEnd();
                reader.Close();
//                if (strData.Contains("Error"))
//                {
//                    Exception e = new Exception(strData);
//                    throw e;
//                }
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR: Exception reading from webpage " + URL);
                strData = "ERROR: Exception reading from webpage";
            }
            return strData; */
        }

        // Thread responsible for extracting the all of the cities links for a given website
        public void ExtractingCities()
        {
            string read;
            AtTheEnd = "";

            // Some weblinks contains dots (.) and .NET simply remove it from URLs. The following code was included just to preserv the dot on links
            MethodInfo getSyntax = typeof(UriParser).GetMethod("GetSyntax", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            FieldInfo flagsField = typeof(UriParser).GetField("m_Flags", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (getSyntax != null && flagsField != null)
            {
                foreach (string scheme in new[] { "http", "https" })
                {
                    UriParser parser = (UriParser)getSyntax.Invoke(null, new object[] { scheme });
                    if (parser != null)
                    {
                        int flagsValue = (int)flagsField.GetValue(parser);
                        // Clear the CanonicalizeAsFilePath attribute
                        if ((flagsValue & 0x1000000) != 0)
                            flagsField.SetValue(parser, flagsValue & ~0x1000000);
                    }
                }
            }

            read = DownloadData(this.oneWebsite.data[0]);
           
            StreamWriter writer;
            SqlConnection myConnection;

            string temp = this.oneWebsite.data[0];
            temp = temp.Replace(".", "_");
            temp = temp.Replace("/", "_");
            temp = temp.Replace(":", "");
            temp = temp.Replace("?", "");
            temp = temp.Replace("=", "");
            writer = File.CreateText(@"C:\Users\MediaConnect\Documents\" + temp + "_out.txt");

            if (read == "ERROR: Exception reading from webpage")
            {
                writer.WriteLine("ERROR: Exception reading from webpage" + this.oneWebsite.data[0]);
                writer.Close();
                return;
            }

            string str;
            string[] parts;
            List<string> listOfCities;
            dealslist listOfEvaluatedDeals = new dealslist();
            List<Tags> listOfDeals = new List<Tags>();

 //           writer.Write(read);
 //           writer.Close();

            Console.WriteLine(@"C:\Users\MediaConnect\Documents\" + this.oneWebsite.data[0] + " file opened!");
            Console.WriteLine("===========================");

            // Check if the Website is valid
            str = this.oneWebsite.data[1];
            if (!WebsiteValid(str, read))
            {
                Console.WriteLine("ERROR: Invalid initial website: " + this.oneWebsite.data[0]);
                writer.WriteLine("ERROR: Invalid initial website: " + this.oneWebsite.data[0]);
                writer.Close();
                return; 
            }

            str = this.oneWebsite.data[2];
            temp = this.SingleDataExtraction(str, read);
            if ((temp == "{:-(") || (temp == ""))
            {
                Console.WriteLine("ERROR: Couldn't find cities in website " + this.oneWebsite.data[0]);
                writer.WriteLine("ERROR: Couldn't find cities in website " + this.oneWebsite.data[0]);
                writer.Close();
                return;
            }
            string sourceLocations = read;
            parts = temp.Split('\n');
            listOfCities = new List<string>();
            for (int i=0; i<parts.Length; i++)
            {
                if ((parts[i] != "") && (!listOfCities.Contains(parts[i])))
                {
                    listOfCities.Add(parts[i]);
                }
            }

            writer.WriteLine("Website | xx | ListOfCities | yy | DealID | DealLinkURL | Category | Company | CompanysURL | Image | Description | Latitude | Longitude | CompleteAddress | StreetName | City | PostalCode | Country | Map | CompanysPhone | RegularPrice | OurPrice | Save | Discount | PayOutAmount | PayOutLink | SecondsTotal | SecondsElapsed | RemainingTime | ExpiryTime | MaxNumberOfVouchers | MinNumberOfVouchers | DealSoldOut | DealEnded | DealValid | PaidVoucherCount | Highlights | BuyDetails | DealText | Reviews | RelatedDeals (same company)");
            List<string> TryLater = new List<string>();
            foreach (string item in listOfCities)
            {
                List<string> SideDeals = new List<string>();
                List<string> EvaluatedSideDeals = new List<string>();
                List<string> SpecialDeals = new List<string>();
                string part_URL = item;
                string URL = "";
                int tries = 3;
                Boolean FirstTime = true;
  //SPECIAL              Boolean hasSpecialdeals = false;

                do
                {   
                    string DealID = "";
                    int i;
                    Tags DealData = new Tags();
                    DateTimeOffset extractedTime;

                    if (SideDeals.Count() > 0)
                    {
                        part_URL = SideDeals.ElementAt(0);
                        EvaluatedSideDeals.Add(part_URL);
                        SideDeals.RemoveAt(0);
                    }

     //               URL = "http://www.dealticker.com/product.php/product_id/17741";
                    URL = baseAddress.Replace("$", part_URL);
                    // opening Website
                    read = DownloadData(URL);
                    extractedTime = DateTimeOffset.Now;

                    if (read == "ERROR: Exception reading from webpage")
                    {
                        TryLater.Add(part_URL);
                        //                       continue;
                    }
                    else
                    {
                        // checking if website is valid or if it has a deal
                        str = this.oneWebsite.data[1];
                        if (!WebsiteValid(str, read))
                        {
                            Console.WriteLine("WARNING: Invalid website: " + URL);
                            writer.WriteLine("WARNING: Invalid website: " + URL);
                            AtTheEnd = AtTheEnd + "WARNING: Invalid website: " + URL + "\n";
                            //                            continue;
                        }
                        else
                        {
                            //  extract the side deals
                            string relatedDeals = "";
                            str = this.oneWebsite.data[3];
                            temp = this.SingleDataExtraction(str, read);
                            str = this.oneWebsite.data[40]; //related deals are handled as sidedeals
                            relatedDeals = this.SingleDataExtraction(str, read);
                            if ((relatedDeals != "{:-(") && (relatedDeals != ""))
                                if (temp != "{:-(")
                                    temp = temp + relatedDeals;
                                else
                                    temp = relatedDeals;
                            if (temp != "{:-(")
                            {
                                List<string> tempSideDeals;
                                parts = temp.Split('\n');
                                tempSideDeals = new List<string>(parts);
                                foreach (string s in tempSideDeals)
                                {
                                    if ((s != "") && (s != part_URL) && (!SideDeals.Contains(s)))
                                    {
                                        if (!EvaluatedSideDeals.Contains(s))
                                        {
                                            // checking if this sidedeal was an evaluated deal. To do that, it must be possible to get DealID from Sidedeal's link
                                            str = this.oneWebsite.data[41];
                                            if (str == "")
                                            {
                                                SideDeals.Add(s);
                                            }
                                            else
                                            {
                                                int c = 0;
                                                int read_pos = 0;
                                                keywords search = new keywords();
                                                keywords end = new keywords();
                                                string tempID = "";
                                                if (str[0] == '?')
                                                {
                                                    c += 1;
                                                    search = GetSearchString(str, ref c, DealData, s);
                                                    while ((c < str.Length) && ((str[c] == ' ') || (str[c] == ';')))
                                                    {
                                                        c += 1;
                                                    }
                                                }
                                                if ((c < str.Length) && (str[c] == '@'))
                                                {
                                                        c += 1;
                                                        end = GetEndString(str, ref c);
                                                }
                                                int end_pos;
                                                while (search.GetTimes() > 0)
                                                {
                                                    search.SetTimes(search.GetTimes() - 1);
                                                    read_pos = s.IndexOf(search.GetKeyword(), read_pos);
                                                    if (read_pos != -1)
                                                        read_pos += search.GetKeyword().Length;
                                                    else
                                                        break;
                                                }
                                                if (read_pos != -1)
                                                {
                                                    if (end.GetKeyword() == "")
                                                        end_pos = s.Length;
                                                    else
                                                    {
                                                        end_pos = read_pos;
                                                        end_pos = s.IndexOf(end.GetKeyword(), end_pos);
                                                    }
                                                    if (end_pos != -1)
                                                    {
                                                        tempID = s.Substring(read_pos, end_pos - read_pos);
                                                    }
                                                }
                                                // reusing c
                                                c = listOfEvaluatedDeals.DealEvaluated(tempID);
                                                if (c != -1)
                                                {
                                                    if (!listOfEvaluatedDeals.GetDealDetails(c).GetListCities().Contains(item))
                                                    {
                                                        listOfEvaluatedDeals.AddCity(c, item);
                                                    }
                                                }
                                                else
                                                    SideDeals.Add(s);
                                            }
                                        }
                                    }
                                }
                            }
                            // Some deal sites have an initial page with many deals. If that is the case, it should be included in DontHandleFirstPage list.
                            // If the dealsite is not in the list, it means that the first page is a deal to be handled.
                            if ((FirstTime) && (!this.DontHandleFirstPage.Contains(this.oneWebsite.data[0])))
                                FirstTime = false;

                            if (FirstTime)
                            {
                                FirstTime = false;
 /*SPECIAL                               str = this.oneWebsite.data[48];
                                temp = this.SingleDataExtraction(str, read);
                                if ((temp != "{:-(") && (temp != ""))
                                {
                                    parts = temp.Split('\n');
                                    SpecialDeals = new List<string>();
                                    hasSpecialdeals = true;
                                    for (int ind = 0; ind < parts.Length; ind++)
                                    {
                                        if ((parts[ind] != "") && (!SpecialDeals.Contains(parts[ind])))
                                        {
                                            if (parts[ind].Contains("http://"))
                                                SpecialDeals.Add(parts[ind]);
                                            else
                                                SpecialDeals.Add(item + parts[ind]);
                                        }
                                    }
                                } */
                            }
                            else
                            {

                                // get the dealID 
                                str = this.oneWebsite.data[4];
                                DealID = this.SingleDataExtraction(str, read);
                                if (DealID == "{:-(")
                                {
                                    Console.WriteLine("WARNING: Couldn't find the DealID in website " + URL);
                                    writer.WriteLine("WARNING: Couldn't find the DealID in website " + URL);
                                    AtTheEnd = AtTheEnd + "WARNING: Couldn't find the DealID in website " + URL + "\n";
                                    //                                continue;
                                }
                                else
                                {
                                    string alternativeID = this.SingleDataExtraction(this.oneWebsite.data[42], read);
                                    if (alternativeID == "")
                                        i = listOfEvaluatedDeals.DealEvaluated(DealID);
                                    else
                                        i = listOfEvaluatedDeals.DealEvaluated(alternativeID);

                                    // check if the deal was evaluated before. If not, store in Deals list
                                    if (i != -1)
                                    {
                                        if (!listOfEvaluatedDeals.GetDealDetails(i).GetListCities().Contains(item))
                                        {
                                            listOfEvaluatedDeals.AddCity(i, item);
                                        }
                                        //                                    continue;
                                    }
                                    else
                                    {

                                        listOfEvaluatedDeals.SetDealID(DealID, alternativeID, item);
                                        DealData.data[0] = this.oneWebsite.data[0];
                                        DealData.data[4] = DealID;

                                        // DealData.data[1] will contain the extracted time. Index 1 is associated to invalid page. If page is valid, the extracted time will be stored
                                        DealData.data[1] = extractedTime.ToString();

                                        Console.WriteLine(baseAddress.Replace("$", "") + " - " + item + " \tDealID - " + DealID);

                                        // Get the data / write to file
                                        for (int j = 5; j < 50; j++)
                                        {
                                            //                                            Console.Write(j + " ");
                                            //                                            if ((j == 13))
                                            //                                            {
                                            //                                                Console.Write("");
                                            //                                           }
                                            if (j == 40)
                                                j = 43;
                                            if (DealData.data[j] == "")
                                            {
                                                int read_pos = 0;
                                                str = this.oneWebsite.data[j];
                                                RecursList.Add(j);
                                                temp = this.SingleDataExtraction(str, read, DealData, ref read_pos);
                                                RecursList.Remove(j);
                                                // Data not expected: in case the extracted data is not the expected one, keep searching
                                                if (j == 8)
                                                {
                                                    int has = temp.IndexOf("youtube.com");
                                                    if (has == -1)
                                                        has = temp.IndexOf("wikipedia");
                                                    if (has != -1)
                                                    {
                                                        str = ReduceInstruc(str);
                                                    }
                                                    while ((has != -1) && (read_pos != -1))
                                                    {
                                                        RecursList.Add(j);
                                                        temp = this.SingleDataExtraction(str, read, DealData, ref read_pos);
                                                        RecursList.Remove(j);
                                                        has = temp.IndexOf("youtube.com");
                                                        if (has == -1)
                                                            has = temp.IndexOf("wikipedia");
                                                    }
                                                }
                                                if ((j < 36) || (j > 39))
                                                {
                                                    temp = temp.Replace("\n", ";");
                                                    temp = temp.Replace("\t", " ");
                                                }
                                                temp = temp.Replace((char)8206, ' ');
                                                while (temp.IndexOf("  ") != -1)
                                                    temp = temp.Replace("  ", " ");
                                                while (temp.IndexOf(" ,") != -1)
                                                    temp = temp.Replace(" ,", ",");
                                                while (temp.IndexOf(",,") != -1)
                                                    temp = temp.Replace(",,", ",");
                                                while (temp.IndexOf(" ;") != -1)
                                                    temp = temp.Replace(" ;", ";");
                                                if (j != 13)
                                                {
                                                    while (temp.IndexOf(";;") != -1)
                                                        temp = temp.Replace(";;", ";");
                                                }
                                                DealData.data[j] = temp;
                                            }
                                        }
                                        if ((DealData.data[17] == "") || (DealData.data[17] == "{:-("))
                                        {
                                            str = this.oneWebsite.data[17];
                                            RecursList.Add(17);
                                            str = this.SingleDataExtraction(str, sourceLocations, DealData);
                                            RecursList.Remove(17);
                                            DealData.data[17] = str;
                                        }
                                        listOfDeals.Add(DealData);
                                    }
                                }
                            }
                        }
                    }
/*SPECIAL                    if ((hasSpecialdeals) && (SideDeals.Count == 0))
                    {
                        if (SpecialDeals.Count > 0)
                        {
                            part_URL = SpecialDeals.ElementAt(0);
                            SpecialDeals.RemoveAt(0);
                        }
                        else
                            hasSpecialdeals = false;
                    } */
//SPECIAL                    if ((!hasSpecialdeals) && (SideDeals.Count == 0) && (TryLater.Count != 0))
                    if ((SideDeals.Count == 0) && (TryLater.Count != 0))
                        {
                        if (tries > 0)
                        {
                            while (TryLater.Count > 0)
                            {
                                SideDeals.Add(TryLater.ElementAt(0));
                                TryLater.RemoveAt(0);
                            }
                            tries -= 1;
                        }
                        else 
                        {
                            foreach (string TryItem in TryLater)
                            {
                                Console.WriteLine("ERROR: Giving up link: " + TryItem);
                                writer.WriteLine("ERROR: Giving up link: " + TryItem);
                                AtTheEnd = AtTheEnd + "ERROR: Giving up link: " + TryItem + "\n";
                            }
                            TryLater = new List<string>();
                        }
                    }
//SPECIAL                } while ((SideDeals.Count() > 0) || (hasSpecialdeals));
                } while (SideDeals.Count() > 0);
            }
            Console.WriteLine("\n\nNow listing cities with the same deal:");
            writer.WriteLine("\n\n\n\nNow listing cities with the same deal:");
            for (int i = 0; i < listOfEvaluatedDeals.CountDeals(); i++)
            {
                deals Dealdetails = listOfEvaluatedDeals.GetDealDetails(i);
                Tags dealData = new Tags();
                string ID = Dealdetails.GetDealID();
                Console.Write("\n" + ID + " - ");
                writer.Write("\n" + ID + " - ");

                foreach (Tags dd in listOfDeals)
                    if (dd.data[4] == ID)
                    {
                        dealData = dd;
                        break;
                    }
                foreach (string item in Dealdetails.GetListCities())
                {
                    dealData.data[2] = dealData.data[2] + item + "; ";
                    Console.Write(item + "  ");
                    writer.Write(item + "  ");
                }
            }
            Console.WriteLine("\n\nTotal of deals: " + listOfEvaluatedDeals.CountDeals());
            Console.WriteLine("Total of cities: " + listOfCities.Count);
            Console.WriteLine();
            writer.WriteLine("\n\n\n\nTotal of deals: " + listOfEvaluatedDeals.CountDeals());
            writer.WriteLine("Total of cities: " + listOfCities.Count + "\n\n\n");



            myConnection = new SqlConnection("server=MEDIACONNECT-PC\\MCAPPS; Trusted_Connection=yes; database=Deals; connection timeout=15");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                myConnection = new SqlConnection("server=FIVEFINGERFINDS\\MEDIACONNECT; Trusted_Connection=yes; database=Deals; connection timeout=15");
                try
                {
                    myConnection.Open();
                }
                catch (Exception error)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine(error.ToString());
                }
            }


// Store the data into SQL Database. Clean and handle the data, if needed
            foreach (Tags dd in listOfDeals)
            {
                string line = "";
                dealer locations = new dealer();

// Data Handling

                // if there is no DealLinkURL, the deal is invalid. So, go to the next deal
                if ((dd.data[5] == "") || (dd.data[5] == "{:-("))
                    continue;

                for (int i = 1; i < 50; i++)
                {
                    int b = dd.data[i].IndexOf("||");
                    if (b != -1)
                        dd.data[i] = dd.data[i].Remove(b);
                    if (dd.data[i] == "{:-(")
                        dd.data[i] = "";
                    RemoveSpaces(ref dd.data[i], true);
                    if(dd.data[i]==";") 
                        dd.data[i] = "";
                }

                if (dd.data[5].ToLower() == "http://")
                    dd.data[5] = "";

                if (dd.data[8].ToLower() == "http://")
                    dd.data[8] = "";

                if (dd.data[8].ToLower() == "%22http:")
                    dd.data[8] = "";

                if (dd.data[9].ToLower() == "http://")
                    dd.data[9] = "";

                if (dd.data[18].ToLower() == "http://")
                    dd.data[18] = "";

                DealersPreProcessingData(dd);

                // If it is an online deal (i.e., there is no address, postal code, remove province, country and city. The advertised cities can be caught from OtherData.ListOfCities table
                if ((dd.data[13] == "") && (dd.data[14] == "") && (dd.data[16] == ""))
                {
                    dd.data[15] = "";
                    dd.data[17] = "";
                    dd.data[43] = "";
                }

                if (dd.data[13] != "")
                    locations = evalutatingFullAddress(dd);

                PriceHandling(dd);
                isDealValid(ref dd.data[32], ref dd.data[33], ref dd.data[34]);
                GetExpiryTime(dd, ref AtTheEnd);
                VouchersHandling(dd);
// end of Data Handling

                SqlCommand myCommandDeal = null;
                SqlCommand myCommandOtherData = null;
                Boolean inList = false;

                try
                {
                    string DealValid = "";
                    string query = "";

                    if (dd.data[34] == "false")
                        query = "SELECT DealsEnded.Website, DealsEnded.DealID, DealValid FROM DealsEnded, OtherData WHERE DealsEnded.Website = OtherData.Website AND DealsEnded.DealID = OtherData.DealID AND DealsEnded.Website = @Website AND DealsEnded.DealID = @DealID";
                    else
                        query = "SELECT DealsList.Website, DealsList.DealID, DealValid FROM DealsList, OtherData WHERE DealsList.Website = OtherData.Website AND DealsList.DealID = OtherData.DealID AND DealsList.Website = @Website AND DealsList.DealID = @DealID";

                    using (SqlCommand myCommandChecker1 = new SqlCommand(query, myConnection))
                    {
                        SqlParameter checkWebsite = new SqlParameter();
                        checkWebsite.ParameterName = "@Website";
                        if ((dd.data[0] == "") || (dd.data[0] == "{:-("))
                            checkWebsite.Value = DBNull.Value;
                        else
                            checkWebsite.Value = dd.data[0];
                        myCommandChecker1.Parameters.Add(checkWebsite);
                        
                        SqlParameter checkDealID = new SqlParameter();
                        checkDealID.ParameterName = "@DealID";
                        if ((dd.data[4] == "") || (dd.data[4] == "{:-("))
                            checkDealID.Value = DBNull.Value;
                        else
                            checkDealID.Value = dd.data[4];
                        myCommandChecker1.Parameters.Add(checkDealID);

                        using (SqlDataReader myChecker1 = myCommandChecker1.ExecuteReader())
                        {
                            if (myChecker1.HasRows)
                            {
                                myChecker1.Read();
                                inList = true;
                                DealValid = myChecker1["DealValid"].ToString();
                            }
                        }
                    }    

                    if (!inList)
                    {
                        if (dd.data[34] == "false")
                            query = "SELECT DealsList.Website, DealsList.DealID, DealValid FROM DealsList, OtherData WHERE DealsList.Website = OtherData.Website AND DealsList.DealID = OtherData.DealID AND DealsList.Website = @Website AND DealsList.DealID = @DealID";
                        else
                            query = "SELECT DealsEnded.Website, DealsEnded.DealID, DealValid FROM DealsEnded, OtherData WHERE DealsEnded.Website = OtherData.Website AND DealsEnded.DealID = OtherData.DealID AND DealsEnded.Website = @Website AND DealsEnded.DealID = @DealID";

                        using (SqlCommand myCommandChecker2 = new SqlCommand(query, myConnection))
                        {
                            SqlParameter checkWebsite = new SqlParameter();
                            checkWebsite.ParameterName = "@Website";
                            if ((dd.data[0] == "") || (dd.data[0] == "{:-("))
                                checkWebsite.Value = DBNull.Value;
                            else
                                checkWebsite.Value = dd.data[0];
                            myCommandChecker2.Parameters.Add(checkWebsite);
                        
                            SqlParameter checkDealID = new SqlParameter();
                            checkDealID.ParameterName = "@DealID";
                            if ((dd.data[4] == "") || (dd.data[4] == "{:-("))
                                checkDealID.Value = DBNull.Value;
                            else
                                checkDealID.Value = dd.data[4];
                            myCommandChecker2.Parameters.Add(checkDealID);

                            using (SqlDataReader myChecker2 = myCommandChecker2.ExecuteReader())
                            {
                                if (myChecker2.HasRows)
                                {
                                    myChecker2.Read();
                                    inList = true;
                                    DealValid = myChecker2["DealValid"].ToString();
                                }
                            }
                        }
                    }

                    if (inList)
                    { // check if deal changed from valid to not valid or vice-versa
                        if (dd.data[34] == DealValid)
                        {
                            if (dd.data[34] == "false")
                            {
                                myCommandDeal = new SqlCommand("UPDATE DealsEnded SET DealLinkURL = @DealLinkURL, Category = @Category, Image = @Image, Description = @Description, DealerID = @DealerID, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, PayOutAmount = @PayOutAmount, PayOutLink = @PayOutLink, ExpiryTime = @ExpiryTime, MaxNumberVouchers = @MaxNumberOfVouchers, MinNumberVouchers = @MinNumberOfVouchers, PaidVoucherCount = @PaidVoucherCount, DealExtractedTime = @DealExtractedTime, Highlights = @Highlights, BuyDetails = @BuyDetails, DealText = @DealText, Reviews = @Reviews, DealSite = @DealSite WHERE Website = @Website AND DealID = @DealID", myConnection);
                                myCommandOtherData = new SqlCommand("UPDATE OtherData SET ListOfCities = @ListOfCities, SideDeals = @SideDeals, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, SecondsTotal = @SecondsTotal, SecondsElapsed = @SecondsElapsed, RemainingTime = @RemainingTime, ExpiryTime = @ExpiryTime, DealSoldOut = @DealSoldOut, DealEnded = @DealEnded, DealValid = @DealValid, RelatedDeals = @RelatedDeals WHERE Website = @Website AND DealID = @DealID", myConnection);
                            }
                            else if (dd.data[34] == "true")
                            {
                                myCommandDeal = new SqlCommand("UPDATE DealsList SET DealLinkURL = @DealLinkURL, Category = @Category, Image = @Image, Description = @Description, DealerID = @DealerID, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, PayOutAmount = @PayOutAmount, PayOutLink = @PayOutLink, ExpiryTime = @ExpiryTime, MaxNumberVouchers = @MaxNumberOfVouchers, MinNumberVouchers = @MinNumberOfVouchers, PaidVoucherCount = @PaidVoucherCount, DealExtractedTime = @DealExtractedTime, Highlights = @Highlights, BuyDetails = @BuyDetails, DealText = @DealText, Reviews = @Reviews, DealSite = @DealSite WHERE Website = @Website AND DealID = @DealID", myConnection);
                                myCommandOtherData = new SqlCommand("UPDATE OtherData SET ListOfCities = @ListOfCities, SideDeals = @SideDeals, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, SecondsTotal = @SecondsTotal, SecondsElapsed = @SecondsElapsed, RemainingTime = @RemainingTime, ExpiryTime = @ExpiryTime, DealSoldOut = @DealSoldOut, DealEnded = @DealEnded, DealValid = @DealValid, RelatedDeals = @RelatedDeals WHERE Website = @Website AND DealID = @DealID", myConnection);
                            }
                        }
                        else
                        {
                            SqlCommand DeleteDeal = null;
                            if (dd.data[34] == "false")
                            {
                                myCommandDeal = new SqlCommand("INSERT INTO DealsEnded (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, Saved, Discount, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews, DealSite) Values (@Website, @DealID, @DealLinkURL, @Category, @Image, @Description, @DealerID, @RegularPrice, @OurPrice, @Saved, @Discount, @PayOutAmount, @PayOutLink, @ExpiryTime, @MaxNumberOfVouchers, @MinNumberOfVouchers, @PaidVoucherCount, @DealExtractedTime, @Highlights, @BuyDetails, @DealText, @Reviews, @DealSite)", myConnection);
                                myCommandOtherData = new SqlCommand("UPDATE OtherData SET ListOfCities = @ListOfCities, SideDeals = @SideDeals, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, SecondsTotal = @SecondsTotal, SecondsElapsed = @SecondsElapsed, RemainingTime = @RemainingTime, ExpiryTime = @ExpiryTime, DealSoldOut = @DealSoldOut, DealEnded = @DealEnded, DealValid = @DealValid, RelatedDeals = @RelatedDeals WHERE Website = @Website AND DealID = @DealID", myConnection);
                                DeleteDeal = new SqlCommand("DELETE FROM DealsList WHERE Website = @Website AND DealID = @DealID", myConnection);
                            }
                            else
                            {
                                myCommandDeal = new SqlCommand("INSERT INTO DealsList (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, Saved, Discount, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews, DealSite) Values (@Website, @DealID, @DealLinkURL, @Category, @Image, @Description, @DealerID, @RegularPrice, @OurPrice, @Saved, @Discount, @PayOutAmount, @PayOutLink, @ExpiryTime, @MaxNumberOfVouchers, @MinNumberOfVouchers, @PaidVoucherCount, @DealExtractedTime, @Highlights, @BuyDetails, @DealText, @Reviews, @DealSite)", myConnection);
                                myCommandOtherData = new SqlCommand("UPDATE OtherData SET ListOfCities = @ListOfCities, SideDeals = @SideDeals, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, SecondsTotal = @SecondsTotal, SecondsElapsed = @SecondsElapsed, RemainingTime = @RemainingTime, ExpiryTime = @ExpiryTime, DealSoldOut = @DealSoldOut, DealEnded = @DealEnded, DealValid = @DealValid, RelatedDeals = @RelatedDeals WHERE Website = @Website AND DealID = @DealID", myConnection);
                                DeleteDeal = new SqlCommand("DELETE FROM DealsEnded WHERE Website = @Website AND DealID = @DealID", myConnection);
                            }

                            SqlParameter checkWebsite = new SqlParameter();
                            checkWebsite.ParameterName = "@Website";
                            if ((dd.data[0] == "") || (dd.data[0] == "{:-("))
                                checkWebsite.Value = DBNull.Value;
                            else
                                checkWebsite.Value = dd.data[0];
                            DeleteDeal.Parameters.Add(checkWebsite);
                        
                            SqlParameter checkDealID = new SqlParameter();
                            checkDealID.ParameterName = "@DealID";
                            if ((dd.data[4] == "") || (dd.data[4] == "{:-("))
                                checkDealID.Value = DBNull.Value;
                            else
                                checkDealID.Value = dd.data[4];
                            DeleteDeal.Parameters.Add(checkDealID);

                            DeleteDeal.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        if (dd.data[34] == "false")
                        {
                            myCommandDeal = new SqlCommand("INSERT INTO DealsEnded (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, Saved, Discount, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews, DealSite) Values (@Website, @DealID, @DealLinkURL, @Category, @Image, @Description, @DealerID, @RegularPrice, @OurPrice, @Saved, @Discount, @PayOutAmount, @PayOutLink, @ExpiryTime, @MaxNumberOfVouchers, @MinNumberOfVouchers, @PaidVoucherCount, @DealExtractedTime, @Highlights, @BuyDetails, @DealText, @Reviews, @DealSite)", myConnection);
                            myCommandOtherData = new SqlCommand("INSERT INTO OtherData (Website, DealID, ListOfCities, SideDeals, RegularPrice, OurPrice, Saved, Discount, SecondsTotal, SecondsElapsed, RemainingTime, ExpiryTime, DealSoldOut, DealEnded, DealValid, RelatedDeals) Values (@Website, @DealID, @ListOfCities, @SideDeals, @RegularPrice, @OurPrice, @Saved, @Discount, @SecondsTotal, @SecondsElapsed, @RemainingTime, @ExpiryTime, @DealSoldOut, @DealEnded, @DealValid, @RelatedDeals)", myConnection);
                        }
                        else
                        {
                            myCommandDeal = new SqlCommand("INSERT INTO DealsList (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, Saved, Discount, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews, DealSite) Values (@Website, @DealID, @DealLinkURL, @Category, @Image, @Description, @DealerID, @RegularPrice, @OurPrice, @Saved, @Discount, @PayOutAmount, @PayOutLink, @ExpiryTime, @MaxNumberOfVouchers, @MinNumberOfVouchers, @PaidVoucherCount, @DealExtractedTime, @Highlights, @BuyDetails, @DealText, @Reviews, @DealSite)", myConnection);
                            myCommandOtherData = new SqlCommand("INSERT INTO OtherData (Website, DealID, ListOfCities, SideDeals, RegularPrice, OurPrice, Saved, Discount, SecondsTotal, SecondsElapsed, RemainingTime, ExpiryTime, DealSoldOut, DealEnded, DealValid, RelatedDeals) Values (@Website, @DealID, @ListOfCities, @SideDeals, @RegularPrice, @OurPrice, @Saved, @Discount, @SecondsTotal, @SecondsElapsed, @RemainingTime, @ExpiryTime, @DealSoldOut, @DealEnded, @DealValid, @RelatedDeals)", myConnection);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                SqlParameter p41 = new SqlParameter();
                p41.ParameterName = "@DealerID";
                p41.Value = getDealerID(dd.data, myConnection, writer, ref AtTheEnd, locations);
                if ((p41.Value.ToString() == "") || (p41.Value.ToString() == "{:-("))
                    p41.Value = DBNull.Value;
                myCommandDeal.Parameters.Add(p41);
                
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@Website";
                if ((dd.data[0] == "") || (dd.data[0] == "{:-("))
                    p1.Value = DBNull.Value;
                else
                    p1.Value = dd.data[0];
                myCommandDeal.Parameters.Add(p1);
                
                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@DealID";
                if ((dd.data[4] == "") || (dd.data[4] == "{:-("))
                    p2.Value = DBNull.Value;
                else
                    p2.Value = dd.data[4];
                myCommandDeal.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@DealLinkURL";
                if ((dd.data[5] == "") || (dd.data[5] == "{:-("))
                    p3.Value = DBNull.Value;
                else
                    p3.Value = dd.data[5];
                myCommandDeal.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@Category";
                if ((dd.data[6] == "") || (dd.data[6] == "{:-("))
                    p4.Value = DBNull.Value;
                else
                   p4.Value = dd.data[6];
                myCommandDeal.Parameters.Add(p4);

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@Image";
                if ((dd.data[9] == "") || (dd.data[9] == "{:-("))
                    p7.Value = DBNull.Value;
                else
                    p7.Value = dd.data[9];
                myCommandDeal.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@Description";
                if ((dd.data[10] == "") || (dd.data[10] == "{:-("))
                    p8.Value = DBNull.Value;
                else
                    p8.Value = dd.data[10];
                myCommandDeal.Parameters.Add(p8);

                SqlParameter p18 = new SqlParameter();
                p18.ParameterName = "@RegularPrice";
                if ((dd.data[20] == "") || (dd.data[20] == "{:-("))
                    p18.Value = DBNull.Value;
                else
                    p18.Value = decimal.Parse(dd.data[20]);
                myCommandDeal.Parameters.Add(p18);

                SqlParameter p19 = new SqlParameter();
                p19.ParameterName = "@OurPrice";
                if ((dd.data[21] == "") || (dd.data[21] == "{:-("))
                    p19.Value = DBNull.Value;
                else
                    p19.Value = decimal.Parse(dd.data[21]);
                myCommandDeal.Parameters.Add(p19);

                SqlParameter p20 = new SqlParameter();
                p20.ParameterName = "@Saved";
                if ((dd.data[22] == "") || (dd.data[22] == "{:-("))
                    p20.Value = DBNull.Value;
                else
                    p20.Value = decimal.Parse(dd.data[22]);
                myCommandDeal.Parameters.Add(p20);

                SqlParameter p21 = new SqlParameter();
                p21.ParameterName = "@Discount";
                if ((dd.data[23] == "") || (dd.data[23] == "{:-("))
                    p21.Value = DBNull.Value;
                else
                    p21.Value = decimal.Parse(dd.data[23]);
                myCommandDeal.Parameters.Add(p21);

                SqlParameter p22 = new SqlParameter();
                p22.ParameterName = "@PayOutAmount";
                if ((dd.data[24] == "") || (dd.data[24] == "{:-("))
                    p22.Value = DBNull.Value;
                else
                    p22.Value = decimal.Parse(dd.data[24]);
                myCommandDeal.Parameters.Add(p22);

                SqlParameter p23 = new SqlParameter();
                p23.ParameterName = "@PayOutLink";
                if ((dd.data[25] == "") || (dd.data[25] == "{:-("))
                    p23.Value = DBNull.Value;
                else
                    p23.Value = dd.data[25];
                myCommandDeal.Parameters.Add(p23);

                SqlParameter p27 = new SqlParameter();
                p27.ParameterName = "@ExpiryTime";
                if ((dd.data[29] == "") || (dd.data[29] == "{:-("))
                    p27.Value = DBNull.Value;
                else
                    p27.Value = DateTimeOffset.Parse(dd.data[29]);
                myCommandDeal.Parameters.Add(p27);

                SqlParameter p28 = new SqlParameter();
                p28.ParameterName = "@MaxNumberOfVouchers";
                if ((dd.data[30] == "") || (dd.data[30] == "{:-("))
                    p28.Value = DBNull.Value;
                else
                    p28.Value = Convert.ToInt32(dd.data[30]);
                myCommandDeal.Parameters.Add(p28);

                SqlParameter p29 = new SqlParameter();
                p29.ParameterName = "@MinNumberOfVouchers";
                if ((dd.data[31] == "") || (dd.data[31] == "{:-("))
                    p29.Value = DBNull.Value;
                else
                    p29.Value = Convert.ToInt32(dd.data[31]);
                myCommandDeal.Parameters.Add(p29);

                SqlParameter p31 = new SqlParameter();
                p31.ParameterName = "@PaidVoucherCount";
                if ((dd.data[35] == "") || (dd.data[35] == "{:-("))
                    p31.Value = DBNull.Value;
                else
                    p31.Value = Convert.ToInt32(dd.data[35]);
                myCommandDeal.Parameters.Add(p31);

                SqlParameter p32 = new SqlParameter();
                p32.ParameterName = "@Highlights";
                if ((dd.data[36] == "") || (dd.data[36] == "{:-("))
                    p32.Value = DBNull.Value;
                else
                    p32.Value = dd.data[36];
                myCommandDeal.Parameters.Add(p32);

                SqlParameter p33 = new SqlParameter();
                p33.ParameterName = "@BuyDetails";
                if ((dd.data[37] == "") || (dd.data[37] == "{:-("))
                    p33.Value = DBNull.Value;
                else
                    p33.Value = dd.data[37];
                myCommandDeal.Parameters.Add(p33);

                SqlParameter p34 = new SqlParameter();
                p34.ParameterName = "@DealText";
                if ((dd.data[38] == "") || (dd.data[38] == "{:-("))
                    p34.Value = DBNull.Value;
                else
                    p34.Value = dd.data[38];
                myCommandDeal.Parameters.Add(p34);

                SqlParameter p35 = new SqlParameter();
                p35.ParameterName = "@Reviews";
                if ((dd.data[39] == "") || (dd.data[39] == "{:-("))
                    p35.Value = DBNull.Value;
                else
                    p35.Value = dd.data[39];
                myCommandDeal.Parameters.Add(p35);

                SqlParameter p42 = new SqlParameter();
                p42.ParameterName = "@DealExtractedTime";
                p42.Value = DateTimeOffset.Parse(dd.data[1]);
                myCommandDeal.Parameters.Add(p42);

                SqlParameter p43 = new SqlParameter();
                p43.ParameterName = "@DealSite";
                if ((dd.data[49] == "") || (dd.data[49] == "{:-("))
                    p43.Value = DBNull.Value;
                else
                    p43.Value = dd.data[49];
                myCommandDeal.Parameters.Add(p43);

                SqlParameter p1a = new SqlParameter();
                p1a.ParameterName = "@Website";
                if ((dd.data[0] == "") || (dd.data[0] == "{:-("))
                    p1a.Value = DBNull.Value;
                else
                    p1a.Value = dd.data[0];
                myCommandOtherData.Parameters.Add(p1a);

                SqlParameter p2a = new SqlParameter();
                p2a.ParameterName = "@DealID";
                if ((dd.data[4] == "") || (dd.data[4] == "{:-("))
                    p2a.Value = DBNull.Value;
                else
                    p2a.Value = dd.data[4];
                myCommandOtherData.Parameters.Add(p2a);

                SqlParameter p18a = new SqlParameter();
                p18a.ParameterName = "@RegularPrice";
                if ((dd.data[20] == "") || (dd.data[20] == "{:-("))
                    p18a.Value = DBNull.Value;
                else
                    p18a.Value = decimal.Parse(dd.data[20]);
                myCommandOtherData.Parameters.Add(p18a);

                SqlParameter p19a = new SqlParameter();
                p19a.ParameterName = "@OurPrice";
                if ((dd.data[21] == "") || (dd.data[21] == "{:-("))
                    p19a.Value = DBNull.Value;
                else
                    p19a.Value = decimal.Parse(dd.data[21]);
                myCommandOtherData.Parameters.Add(p19a);

                SqlParameter p20a = new SqlParameter();
                p20a.ParameterName = "@Saved";
                if ((dd.data[22] == "") || (dd.data[22] == "{:-("))
                    p20a.Value = DBNull.Value;
                else
                    p20a.Value = decimal.Parse(dd.data[22]);
                myCommandOtherData.Parameters.Add(p20a);

                SqlParameter p21a = new SqlParameter();
                p21a.ParameterName = "@Discount";
                if ((dd.data[23] == "") || (dd.data[23] == "{:-("))
                    p21a.Value = DBNull.Value;
                else
                    p21a.Value = decimal.Parse(dd.data[23]);
                myCommandOtherData.Parameters.Add(p21a);

                SqlParameter p24 = new SqlParameter();
                p24.ParameterName = "@SecondsTotal";
                if ((dd.data[26] == "") || (dd.data[26] == "{:-("))
                    p24.Value = DBNull.Value;
                else
                    p24.Value = dd.data[26];
                myCommandOtherData.Parameters.Add(p24);

                SqlParameter p25 = new SqlParameter();
                p25.ParameterName = "@SecondsElapsed";
                if ((dd.data[27] == "") || (dd.data[27] == "{:-("))
                    p25.Value = DBNull.Value;
                else
                    p25.Value = dd.data[27];
                myCommandOtherData.Parameters.Add(p25);

                SqlParameter p26 = new SqlParameter();
                p26.ParameterName = "@RemainingTime";
                if ((dd.data[28] == "") || (dd.data[28] == "{:-("))
                    p26.Value = DBNull.Value;
                else
                    p26.Value = dd.data[28];
                myCommandOtherData.Parameters.Add(p26);

                SqlParameter p27a = new SqlParameter();
                p27a.ParameterName = "@ExpiryTime";
                if ((dd.data[29] == "") || (dd.data[29] == "{:-("))
                    p27a.Value = DBNull.Value;
                else
                    p27a.Value = DateTimeOffset.Parse(dd.data[29]);
                myCommandOtherData.Parameters.Add(p27a);

                SqlParameter p30 = new SqlParameter();
                p30.ParameterName = "@DealValid";
                if ((dd.data[34] == "") || (dd.data[34] == "{:-("))
                    p30.Value = DBNull.Value;
                else
                    p30.Value = dd.data[34];
                myCommandOtherData.Parameters.Add(p30);

                SqlParameter p36 = new SqlParameter();
                p36.ParameterName = "@ListOfCities";
                if ((dd.data[2] == "") || (dd.data[2] == "{:-("))
                    p36.Value = DBNull.Value;
                else
                    p36.Value = dd.data[2];
                myCommandOtherData.Parameters.Add(p36);

                SqlParameter p37 = new SqlParameter();
                p37.ParameterName = "@SideDeals";
                if ((dd.data[3] == "") || (dd.data[3] == "{:-("))
                    p37.Value = DBNull.Value;
                else
                    p37.Value = dd.data[3];
                myCommandOtherData.Parameters.Add(p37);

                SqlParameter p38 = new SqlParameter();
                p38.ParameterName = "@DealSoldOut";
                if ((dd.data[32] == "") || (dd.data[32] == "{:-("))
                    p38.Value = DBNull.Value;
                else
                    p38.Value = dd.data[32];
                myCommandOtherData.Parameters.Add(p38);

                SqlParameter p39 = new SqlParameter();
                p39.ParameterName = "@DealEnded";
                if ((dd.data[33] == "") || (dd.data[33] == "{:-("))
                    p39.Value = DBNull.Value;
                else
                    p39.Value = dd.data[33];
                myCommandOtherData.Parameters.Add(p39);

                SqlParameter p40 = new SqlParameter();
                p40.ParameterName = "@RelatedDeals";
                if ((dd.data[40] == "") || (dd.data[40] == "{:-("))
                    p40.Value = DBNull.Value;
                else
                    p40.Value = dd.data[40];
                myCommandOtherData.Parameters.Add(p40);

                myCommandDeal.ExecuteNonQuery();
                myCommandOtherData.ExecuteNonQuery();
                
                for (int j = 0; j < dd.data.Length; j++)
                {
                    line = line + dd.data[j];
                    if (j < dd.data.Length)
                        line = line + "|";
                    else
                        line = line + "\n\n";
                }
                writer.WriteLine(line);
            }

            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            writer.WriteLine("\n\n" + AtTheEnd);
            writer.Close();

        }

        private dealer evalutatingFullAddress(Tags dd)
        {
            dealer locations = new dealer();
/*            string contact = "";
            string postalCode = "";
            string streetAddress = "";
            string city = "";
            string province = "";
            string country = "";
            string latitude = "";
            string longitude = "";
            string map = "";*/

            if (dd.data[13][0] != '#')
            {
                Console.WriteLine("Missing Address format.\n");
                AtTheEnd = AtTheEnd + "Missiing Address format.\n";
                dd.data[13] = "";
                return null;
            }
            int i = dd.data[13].IndexOf('#',1);
            if (i == -1)
            {
                Console.WriteLine("Wrong Address format.\n");
                AtTheEnd = AtTheEnd + "Wrong Address format.\n";
                dd.data[13] = "";
                return null;
            }
            int format = Convert.ToInt16(dd.data[13].Substring(1, i - 1));
            dd.data[13] = dd.data[13].Remove(0, i + 1);

            if (dd.data[13] == "")
            {
                location dealer_address = new location();
                dealer_address.SetContact(dd.data[19]);
                dealer_address.SetPostalCode(dd.data[16]);
                dealer_address.SetStreetAddress(dd.data[14]);
                dealer_address.SetCity(dd.data[15]);
                dealer_address.SetProvince(dd.data[43]);
                dealer_address.SetCountry(dd.data[17]);
                if ((dd.data[11] == "") && (dd.data[12] == ""))
                {
                    GetLatLong(dealer_address);
                }
                else
                {
                    dealer_address.SetLatitude(dd.data[11]);
                    dealer_address.SetLongitude(dd.data[12]);
                }
                dealer_address.SetMap(CreateMapLink(dd));

                if ((dd.data[14] != "") || (dd.data[15] != "") || (dd.data[43] != "") || (dd.data[17] != "") || (dd.data[16] != "") || (dd.data[19] != "") || (dd.data[11] != "") || (dd.data[12] != "") || (dd.data[18] != ""))
                {
                    locations.SetLocation(dealer_address);
                    dd.data[13] = dd.data[14] + ", " + dd.data[15] + ", " + dd.data[43] + ", " + dd.data[17] + ", " + dd.data[16] + ", " + dd.data[19] + ", " + dd.data[11] + ", " + dd.data[12] + ", " + dd.data[18];
                    return locations;
                }
                else
                    return null;
            }

            if (format == 1) // 1.	City, street, Postal Code, Contact | Contact (EX: DealTicker)
            {
                string fullAddress = dd.data[13];
                string aux;
                dd.data[13] = "";

                while (fullAddress.Length > 0)
                {
                    location dealer_address = new location();
                    i = fullAddress.LastIndexOf(',');
                    if (i == -1)
                        i = 0;
                    aux = fullAddress.Substring(i, fullAddress.Length - i);
                    fullAddress = fullAddress.Remove(i);
                    RemoveSpaces(ref aux, true);
                    dealer_address.SetContact(aux);

                    if (fullAddress.Length > 0)
                    {
                        i = fullAddress.LastIndexOf(',');
                        if (i == -1)
                            i = 0;
                        aux = fullAddress.Substring(i, fullAddress.Length - i);
                        fullAddress = fullAddress.Remove(i);
                        RemoveSpaces(ref aux, true);
                        dealer_address.SetPostalCode(aux);

                        i = fullAddress.LastIndexOf(',');
                        if (i == -1)
                            i = 0;
                        aux = fullAddress.Substring(i, fullAddress.Length - i);
                        fullAddress = fullAddress.Remove(i);
                        RemoveSpaces(ref aux, true);
                        dealer_address.SetStreetAddress(aux);

                        i = fullAddress.LastIndexOf(';');
                        if (i == -1)
                        {
                            i = fullAddress.LastIndexOf(',');
                            if (i == -1)
                                i = 0;
                        }
                        aux = fullAddress.Substring(i, fullAddress.Length - i);
                        fullAddress = fullAddress.Remove(i);
                        RemoveSpaces(ref aux, true);
                        dealer_address.SetCity(aux);

                        if ((fullAddress.Length == 0) && (locations.CountLocations() == 0))
                        {
                            dealer_address.SetProvince(dd.data[43]);
                            dealer_address.SetCountry(dd.data[17]);
                            if ((dd.data[11] == "") && (dd.data[12] == ""))
                            {
                                GetLatLong(dealer_address);
                            }
                            else
                            {
                                dealer_address.SetLatitude(dd.data[11]);
                                dealer_address.SetLongitude(dd.data[12]);
                            }
                            dealer_address.SetMap(CreateMapLink(dd));
                        }
                        else
                        {
                            //  ??? What about Province and Country? ?????????????????????????????????????????????????????
                            AtTheEnd = AtTheEnd + "WARNING: There is no Province/Country...\n";
                            GetLatLong(dealer_address);
                            dealer_address.SetMap(CreateMapLink(dd));
                        }

                        locations.SetLocation(dealer_address);
                        dd.data[13] = dd.data[13] + ", " + dealer_address.GetStreetAddress() + ", " + dealer_address.GetCity() + ", " + dealer_address.GetProvince() + ", " + dealer_address.GetCountry() + ", " + dealer_address.GetPostalCode() + ", " + dealer_address.GetContact() + ", " + dealer_address.GetLatitude() + ", " + dealer_address.GetLongitude() + ", " + dealer_address.GetMap();
                    }
                }
                return locations;
            }
            if (format == 2) // 
            {
            }
            if (format == 3) // 
            {
            }
            if (format == 4) // 
            {
            }
            if (format == 5) // 
            {
            }
            return null;
        }

        private void GetLatLong(location dealer_address)
            // ??? Get latitude and Longitude from Googlemaps, based on the current address ??????????????????????????????
        {
        }

        private string CreateMapLink(Tags dd)
        // Creating/changing the map link
        {
            // based on Latitude and Longitude
            if ((dd.data[11] != "") && (dd.data[12] != ""))
            {
                dd.data[18] = "http://maps.google.com/maps?q=" + dd.data[11] + ", " + dd.data[12];
            }
            return dd.data[18];
        }

        private void DealersPreProcessingData(Tags dd)
        {
            dd.data[8] = dd.data[8].Replace("www.", ""); // let all companie's URL with the same format, i.e., without "www." and the last "/"
            if ((dd.data[8].Length >= 1) && (dd.data[8][dd.data[8].Length - 1] == '/'))
                dd.data[8] = dd.data[8].Substring(0, dd.data[8].Length - 1);
            if (dd.data[8].IndexOf("youtube.com") != -1)
                dd.data[8] = "";

            if ((dd.data[15].Length >= 5) && (dd.data[15].Substring(0, 5).ToLower() == "http:"))
                dd.data[15] = "";
            if ((dd.data[15].Length >= 4) && (dd.data[15].Substring(0, 4).ToLower() == "www."))
                dd.data[15] = "";

            if ((dd.data[18].Length == 30) && (dd.data[18] == "http://maps.google.com/maps?q="))
                dd.data[18] = "";

            if (dd.data[17].ToLower() == "usa")
                dd.data[17] = "United States";
            
            Boolean phone = false;
            for (int i = 13; i <= 43; i++)
            {
                if (i == 16)
                    i = 43;
                string aux1 = dd.data[i].ToLower();
                string aux = aux1;
                if (aux != "")
                {
                    if (i != 13)
                        aux = ExtractPhone(aux, dd, ref phone);
                    aux = RemoveWebLinks(aux);
                    aux = aux.Replace("include photo", "");
                    aux = aux.Replace("(map)", "");
                    aux = aux.Replace("(carte)", "");
                    aux = aux.Replace("phone and contact with:", "");
                    aux = aux.Replace("domocilio conocido", "");
                    aux = aux.Replace("on location shoot", "");
                    aux = aux.Replace("call to order", "");
                    aux = aux.Replace("to place your order", "");
                    aux = aux.Replace("once purchased", "");
                    aux = aux.Replace("mailed to your door", "");
                    aux = aux.Replace("see website for directions", "");
                    aux = aux.Replace("click website link to", "");
                    aux = aux.Replace("to redeem voucher,", "");
                    aux = aux.Replace("to redeem voucher", "");
                    aux = aux.Replace("to redeem your voucher,", "");
                    aux = aux.Replace("to redeem your voucher", "");
                    aux = aux.Replace("to book your appointment", "");
                    aux = aux.Replace("include photo", "");
                    aux = aux.Replace("mailing address and contact number", "");
                    aux = aux.Replace("please visit:", "");
                    aux = aux.Replace("please visit", "");
                    aux = aux.Replace("they come to you", "");
                    aux = aux.Replace("for reservations", "");
                    aux = aux.Replace("redeem online by clicking the \"redemption\" link on your voucher", "");
                    aux = aux.Replace("Redeem online by clicking \"Redemption\" link on your voucher", "");
                    aux = aux.Replace("online redemption:", "");
                    aux = aux.Replace("online redemption", "");
                    aux = aux.Replace("web redemption:", "");
                    aux = aux.Replace("web redemption", "");
                    aux = aux.Replace("or redeem", "");
                    aux = aux.Replace("redeem", "");
                    aux = aux.Replace("online at", "");
                    aux = aux.Replace("online:", "");
                    aux = aux.Replace("online", "");
                    aux = aux.Replace("or by phone:", "");
                    aux = aux.Replace("or by phone", "");
                    aux = aux.Replace("by phone:", "");
                    aux = aux.Replace("by phone", "");
                    aux = aux.Replace("mobile service", "");
                    aux = aux.Replace("mobile service:", "");
                    aux = aux.Replace("gta mobile", "");
                    aux = aux.Replace("mobile", "");
                    aux = aux.Replace("call/email", "");
                    aux = aux.Replace("or by email:", "");
                    aux = aux.Replace("or by e-mail:", "");
                    aux = aux.Replace("or by email", "");
                    aux = aux.Replace("or by e-mail", "");
                    aux = aux.Replace("by emailing:", "");
                    aux = aux.Replace("by emailing", "");
                    aux = aux.Replace("by email:", "");
                    aux = aux.Replace("by email", "");
                    aux = aux.Replace("by e-mail:", "");
                    aux = aux.Replace("by e-mail", "");
                    aux = aux.Replace("for inquiries,", "");
                    aux = aux.Replace("for inquiries", "");
                    aux = aux.Replace("please call:", "");
                    aux = aux.Replace("please call", "");
                    aux = aux.Replace("please", "");
                    aux = aux.Replace("call:", "");
                    aux = aux.Replace("call ", " ");
                    aux = aux.Replace("call\n", "\n");
                    aux = aux.Replace("or email:", "");
                    aux = aux.Replace("or email", "");
                    aux = aux.Replace("email:", "");
                    aux = aux.Replace("email", "");
                    aux = aux.Replace("or e-mail:", "");
                    aux = aux.Replace("or e-mail", "");
                    aux = aux.Replace("e-mail:", "");
                    aux = aux.Replace("e-mail", "");
                    aux = aux.Replace("multiple locations", "");
                    aux = aux.Replace("valid at", "");
                    aux = aux.Replace("view locations", "");
                    aux = aux.Replace("mail out", "");
                    if (aux1 != aux)
                    {
                        RemoveSpaces(ref aux, true);
                        if (aux == "or") aux = "";
                        dd.data[i] = aux;
                    }
                }
            }

            // Put emails in the right column
            if (dd.data[13] != "")
            {
                transferEmails(ref dd.data[13], ref dd.data[19]);
            }
            if (dd.data[14] != "")
            {
                transferEmails(ref dd.data[14], ref dd.data[19]);
            }

            if (dd.data[15] != "")
            {
                transferEmails(ref dd.data[15], ref dd.data[19]);
                if ((dd.data[15] != "") && ((dd.data[15][0] == '(') || ((dd.data[15][0] >= '0') && (dd.data[15][0] <= '9'))))
                {  //??? try using Regex to find telephones in all of the columns
                    // Contacts are in the wrong place. Moving them from City to Contact
                    if (dd.data[19].IndexOf(dd.data[15]) == -1)
                    {
                        dd.data[19] = dd.data[19] + dd.data[15] + "; ";
                    }
                    dd.data[15] = "";
                }
            }

            if ((dd.data[13] == "") && (dd.data[14] == "") && (dd.data[16] == ""))
            {
                if ((dd.data[15] != "") && (dd.data[19] == ""))
                {
                    // Contacts are in the wrong place. Moving them from City to Contact
                    if (dd.data[19] == "")
                        dd.data[19] = dd.data[15];
                    else if (dd.data[19].IndexOf(dd.data[15]) == -1)
                        dd.data[19] = dd.data[19] + "; " + dd.data[15];
                    dd.data[15] = "";
                }
                if (dd.data[43] != "")
                {
                    transferEmails(ref dd.data[43], ref dd.data[19]);
                    if ((dd.data[43] != "") && ((dd.data[43][0] == '(') || ((dd.data[43][0] >= '0') && (dd.data[43][0] <= '9'))))
                    {  //??? try using Regex to find telephones in all of the columns
                        // Contacts are in the wrong place. Moving them from City to Contact
                        if (dd.data[19].IndexOf(dd.data[43]) == -1)
                        {
                            dd.data[19] = dd.data[19] + dd.data[43] + "; ";
                        }
                        dd.data[43] = "";
                    }
                }
            }

            if (dd.data[15] == dd.data[43])
            {
                int pos = dd.data[15].IndexOf(",");
                if (pos != -1)
                {
                    dd.data[15] = dd.data[15].Remove(pos);
                    dd.data[43] = dd.data[43].Remove(0, pos);
                    RemoveSpaces(ref dd.data[15], true);
                    RemoveSpaces(ref dd.data[43], true);
                }
            }
            //  if Latitude contains both Lat and Longitude data, Longitude field is empty
            if ((dd.data[11] != "") && (dd.data[12] == ""))
            {
                SeparateLatLong(ref dd.data[11], ref dd.data[12]);
            }

            // remove latitude and longitude if it points to nowhere
            if ((dd.data[11].Length >= 3) && (dd.data[11].Substring(0, 3) == "56.") && (dd.data[12].Length >= 5) && (dd.data[12].Substring(0, 5) == "-106."))
            {
                dd.data[11] = "";
                dd.data[12] = "";
                if ((dd.data[18] != "") && (dd.data[18].IndexOf("56.") != -1))
                    dd.data[18] = "";
            }
            if ((dd.data[11].Length >= 3) && (dd.data[11].Substring(0, 3) == "51.") && (dd.data[12].Length >= 5) && (dd.data[12].Substring(0, 4) == "-85."))
            {
                dd.data[11] = "";
                dd.data[12] = "";
                if ((dd.data[18] != "") && (dd.data[18].IndexOf("51.") != -1))
                    dd.data[18] = "";
            }

            if ((dd.data[11] != "") && (dd.data[12] != "") && ((Convert.ToDouble(dd.data[11]) == 0) && (Convert.ToDouble(dd.data[12]) == 0)))
            {
                dd.data[11] = "";
                dd.data[12] = "";
            }

            // If googlemaps link has online, it is invalid, so remove it. Also remove Lat/Long
            if (dd.data[18] != "")
            {
                string aux = dd.data[18].ToLower();
                if ((aux.IndexOf("=online+") != -1) || (aux.IndexOf("+online+") != -1) ||
                    (aux.IndexOf("=mobile+") != -1) || (aux.IndexOf("+mobile+") != -1) ||
                    (aux.IndexOf("=mail+out+") != -1) || (aux.IndexOf("+mail+out+") != -1) ||
                    (aux.IndexOf("=mailed+to+your+door+") != -1) || (aux.IndexOf("+mailed+to+your+door+") != -1) ||
                    (aux.IndexOf("=they+come+to+you+") != -1) || (aux.IndexOf("+they+come+to+you+") != -1))
                {
                    dd.data[18] = "";
                    dd.data[11] = "";
                    dd.data[12] = "";
                }
            }

            RoundLatLong(ref dd.data[11], ref dd.data[12], ref AtTheEnd);

        }

        private void VouchersHandling(Tags dd)
        {
            dd.data[35] = dd.data[35].ToLower();
            dd.data[35] = dd.data[35].Replace("buys", "");
            dd.data[35] = dd.data[35].Replace("buy", "");
            dd.data[35] = dd.data[35].Replace("achats", "");
            dd.data[35] = dd.data[35].Replace("achat", "");
            dd.data[35] = dd.data[35].Replace(",", "");

            dd.data[31] = dd.data[31].ToLower();
            dd.data[31] = dd.data[31].Replace("buys", "");
            dd.data[31] = dd.data[31].Replace("buy", "");
            dd.data[31] = dd.data[31].Replace("achats", "");
            dd.data[31] = dd.data[31].Replace("achat", "");
            dd.data[31] = dd.data[31].Replace(",", "");
            dd.data[31] = dd.data[31].Replace("one", "1");

            if (dd.data[30] == "0")
                dd.data[30] = "";

            int i = dd.data[31].IndexOf('+');
            if ((i != -1) && (i + 1 < dd.data[31].Length))
            {
                int n1 = Convert.ToInt32(dd.data[31].Substring(0, i));
                int n2 = Convert.ToInt32(dd.data[31].Substring(i+1, dd.data[31].Length - (i + 1)));
                dd.data[31] = (n1 + n2).ToString();
            }
        }

        private void GetExpiryTime(Tags dd, ref string AtTheEnd)
        {
            if (dd.data[29][0] != '#')
            {
                Console.WriteLine("Missing time format.");
                AtTheEnd = AtTheEnd + "Missiing time format.";
                dd.data[29] = "";
                return;
            }
            int i = dd.data[29].IndexOf('#',1);
            if (i == -1)
            {
                Console.WriteLine("Wrong time format.");
                AtTheEnd = AtTheEnd + "Wrong time format.";
                dd.data[29] = "";
                return;
            }
            int format = Convert.ToInt16(dd.data[29].Substring(1, i - 1));
            dd.data[29] = dd.data[29].Remove(0, i + 1);

            if (format == 1) // represents time by seconds (elapsed and total)
            {
                TimeSpan total = TimeSpan.FromSeconds(Convert.ToDouble(dd.data[26]));
                TimeSpan elapsed = TimeSpan.FromSeconds(Convert.ToDouble(dd.data[27]));
                DateTimeOffset extracted = DateTimeOffset.Parse(dd.data[1]);
                dd.data[28] = (total - elapsed).ToString();
                dd.data[29] = (extracted.Add(total - elapsed)).ToString();
                return;
            }
            if (format == 2)
            {
                int hr, min, sec, day;
                string remaining = dd.data[28];
                TimeSpan time;
                DateTimeOffset extracted = DateTimeOffset.Parse(dd.data[1]);
                DateTimeOffset tomorrow = extracted.Date.AddDays(1);
                hr = min = sec = day = 0;
                remaining = remaining.ToLower();
                remaining = remaining.Replace("jours", "");
                remaining = remaining.Replace("jour", "");
                remaining = remaining.Replace("dagen", "");
                remaining = remaining.Replace("dag", "");
                remaining = remaining.Replace("days", "-");
                remaining = remaining.Replace("day", "-");
                remaining = remaining.Replace("d", "-");
                remaining = remaining.Replace("hours", ":");
                remaining = remaining.Replace("hour", ":");
                remaining = remaining.Replace("hrs", ":");
                remaining = remaining.Replace("hr", ":");
                remaining = remaining.Replace("minutes", ":");
                remaining = remaining.Replace("minute", ":");
                remaining = remaining.Replace("min", ":");
                remaining = remaining.Replace("seconds", "");
                remaining = remaining.Replace("sec", "");
                remaining = remaining.Replace("days", "-");
                remaining = remaining.Replace("day", "-");
                remaining = remaining.Replace("d", "-");
                remaining = remaining.Replace(";", "");
                remaining = remaining.Replace(" ", "");

                i = remaining.IndexOf('-');
                if (i != -1)
                {
                    if (i > 0)
                    {
                        day = Convert.ToInt32(remaining.Substring(0, i));
                    }
                    remaining = remaining.Remove(0, i + 1);
                }
                if (remaining == "")
                {
                    dd.data[29] = (tomorrow.AddDays(day).ToString());
                    dd.data[28] = (DateTimeOffset.Parse(dd.data[29]) - extracted).ToString();
//                    dd.data[29] = tomorrow.ToString();
//                    dd.data[28] = (DateTimeOffset.Parse(dd.data[29]) - DateTimeOffset.Parse(dd.data[1])).ToString();
                    return;
                }
                i = remaining.LastIndexOf(':');
                if (i != -1)
                {
                    if ((i + 1) < remaining.Length)
                    {
                        if (remaining[i + 1] != '-')
                            sec = Convert.ToInt32(remaining.Substring(i + 1, remaining.Length - i - 1));
                        else
                        {
                            if (dd.data[34] == "true")
                                AtTheEnd = AtTheEnd + "ATTENTION: This deal " + dd.data[4] + " from " + dd.data[0] + " probably has ended but is not flagged as ended";
                        }
                        remaining = remaining.Remove(i, remaining.Length - i);
                    }
                    else
                        remaining = remaining.Remove(i, 1);
                }
                else
                {
                    if ((remaining.Length > 0) && (day == 0))
                    {
                        day = Convert.ToInt32(remaining);
                        remaining = "";
                    }
                }
                i = remaining.LastIndexOf(':');
                if (i != -1)
                {
                    if ((i + 1) < remaining.Length)
                    {
                        if (remaining[i + 1] != '-')
                            min = Convert.ToInt32(remaining.Substring(i + 1, remaining.Length - i - 1));
                        remaining = remaining.Remove(i, remaining.Length - i);
                    }
                    else
                        remaining = remaining.Remove(i, 1);
                }
                remaining = remaining.Replace("-","");
                if (remaining.Length > 0)
                {
                    hr = Convert.ToInt32(remaining);
                }
                time = new TimeSpan(day, hr, min, sec);
                dd.data[28] = time.ToString();
                dd.data[29] = extracted.Add(time).ToString();
                return;
            }
            if (format == 3)
            {
                dd.data[29] = dd.data[29].ToUpper();
                dd.data[29] = dd.data[29].Replace("GMT","");
                dd.data[28] = (DateTimeOffset.Parse(dd.data[29]) - DateTimeOffset.Parse(dd.data[1])).ToString();
                return;
            }
            return;
        }

        private void isDealValid(ref string soldOut, ref string ended, ref string isValid)
        {
            // check if the deal is sold out or has ended
            // if isValid has any value, it means that the deal is invalid
            if ((soldOut != "") && (soldOut.ToLower() != "false"))
            {
                soldOut = "true";
            }
            if ((ended != "") && (ended.ToLower() != "false"))
            {
                ended = "true";
            }
            if ((isValid != "") && (isValid.ToLower() != "true"))
                isValid = "false";
            else if ((soldOut.ToLower() == "true") || (ended.ToLower() == "true"))
                isValid = "false";
            else
                isValid = "true";
        }

        private void PriceHandling(Tags dd)
        {
            double our, regular, save, disc;

            for (int i = 20; i < 25; i++) // includes pay out amount
            {
                dd.data[i] = dd.data[i].Replace("$", "");
                int j = dd.data[i].IndexOf(',');
                if ((j != -1) && (j + 3 < dd.data[i].Length))
                {
                    dd.data[i] = dd.data[i].Replace(",", "");
                }
            }

            if (dd.data[20] != "")
                regular = Convert.ToDouble(dd.data[20]);
            else
                regular = 0;

            if (dd.data[21] != "")
                our = Convert.ToDouble(dd.data[21]);
            else
                our = 0;

            if (dd.data[22] != "")
                save = Convert.ToDouble(dd.data[22]);
            else
                save = 0;

            if (dd.data[23] != "")
            {
                dd.data[23] = dd.data[23].Replace("%", "");
                disc = Convert.ToDouble(dd.data[23]);
            }
            else
                disc = 0;

            if ((regular == 0) && ((our != 0) && (disc != 0)))
            {
                regular = Math.Round(((our / (100 - disc)) * 100),2);
                dd.data[20] = regular.ToString();
            }

            if ((save == 0) && ((regular != 0) && (our != 0)))
            {
                save = regular - our;
                dd.data[22] = save.ToString();
            }
        }

        private void RoundLatLong(ref string p, ref string p_2, ref string AtTheEnd)
        {
            // Round Latitude to 4 decimals
            if (p != "")
            {
                string s = p.ToString();
                double lat = 0;
                try
                {
                    lat = Convert.ToDouble(s);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '{0}' to a Double.", s);
                    AtTheEnd = AtTheEnd + "Unable to convert " + s + " to a Double.";
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'{0}' is outside the range of a Double.", s);
                    AtTheEnd = AtTheEnd + s + " is outside the range of a Double.";
                }
                lat = Math.Round(lat, 4);
                p = lat.ToString();
            }

            // Round Longitude to 4 decimals
            if (p_2 != "")
            {
                string s = p_2.ToString();
                double longit = 0;
                try
                {
                    longit = Convert.ToDouble(s);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '{0}' to a Double.", s);
                    AtTheEnd = AtTheEnd + "Unable to convert " + s + " to a Double.";
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'{0}' is outside the range of a Double.", s);
                    AtTheEnd = AtTheEnd + s + " is outside the range of a Double.";
                }
                longit = Math.Round(longit, 4);
                p_2 = longit.ToString();
            }

        }

        private string ReduceInstruc(string str) // reduce search (?) instructions, eliminating the firstsearch (?). Need to do that if the scope were reduced
        {
            int b, e;
            b = str.IndexOf("?\"");
            while (b != -1)
            {
                e = str.IndexOf(";",b);
                if (e == -1)
                    break;
                if ((e + 1 < str.Length) && (str[e + 1] == '?'))
                {
                    str = str.Remove(b, e - b + 1);
//                    str = str.Replace(str.Substring(b, e - b + 1), "");
                }
                b = str.IndexOf("||", b);
                if (b != -1)
                    b = str.IndexOf("?\"", b);
            }
            return str;
        }

        private void transferEmails(ref string aux, ref string contact)
        {
            int e, b = aux.IndexOf("@");
            string temp;
            while (b != -1)
            {
                e = aux.IndexOf(" ", b);
                if (e == -1)
                    e = aux.Length - 1;
                b = aux.LastIndexOf(" ", b);
                if (b == -1)
                    b = 0;
                temp = aux.Substring(b, e - b + 1);
                if (temp.IndexOf("(") == -1)
                {
                    if (contact.IndexOf(temp) == -1)
                    {
                        if ((contact == "") || (contact == "{:-("))
                            contact = temp + ";";
                        else
                            contact = temp + "; " + contact;
                    }
                    e = 0;
                }
                aux = aux.Replace(temp, "");
                RemoveSpaces(ref aux, true);
                b = aux.IndexOf("@", e);
            }
        }

        private string RemoveWebLinks(string aux)
        {
            int b = aux.IndexOf("http://");
            while (b != -1)
            {
                int e = aux.IndexOf(" ", b);
                if (e != -1)
                    aux = aux.Replace(aux.Substring(b, e - b + 1),"");
                else
                    aux = aux.Replace(aux.Substring(b, aux.Length - b),"");
                RemoveSpaces(ref aux, true);
                b = aux.IndexOf("http://");
            }
            b = aux.IndexOf("www.");
            while (b != -1)
            {
                int e = aux.IndexOf(" ", b);
                if (e != -1)
                    aux = aux.Replace(aux.Substring(b, e - b + 1),"");
                else
                    aux = aux.Replace(aux.Substring(b, aux.Length - b),"");
                RemoveSpaces(ref aux, true);
                b = aux.IndexOf("www.");
            }
            b = aux.IndexOf(".com");
            if (b == -1)
                b = aux.IndexOf(".ca");
            while (b != -1)
            {
                int e = b;
                b = aux.LastIndexOf(" ",b);
                if (b == -1)
                    b = 0;
                e = aux.IndexOf(" ", e);
                if (e == -1)
                    e = aux.Length - 1;
                int a = aux.IndexOf("@", b);
                if ((a == -1) || (a >= e)) // it is not an email address
                {
                    aux = aux.Replace(aux.Substring(b, e - b + 1), "");
                    RemoveSpaces(ref aux, true);
                    b = aux.IndexOf(".com");
                    if (b == -1)
                        b = aux.IndexOf(".ca");
                }
                else
                {
                    b = aux.IndexOf(".com", e);
                    if (b == -1)
                        b = aux.IndexOf(".ca", e);
                }
            }
            return aux;
        }

        private string ExtractPhone(string aux, Tags dd, ref Boolean phone) // phone is only true if previous column indicates that the current one probably has a phone number
        {
            int b;
            if (phone == false)
            {
                aux = aux.Replace(" at ", " ");
                b = aux.IndexOf("phone");
                if (b == -1)
                    b = aux.IndexOf("call");
                if (b != -1)
                {
                    if ((b - 1 >= 0) && (((aux[b - 1] >= 'a') && (aux[b - 1] <= 'z')) || ((aux[b - 1] >= 'A') && (aux[b - 1] <= 'Z'))))
                        return aux;
                    b += 4; // lenght of word call
                    if (aux[b] == 'e')
                        b += 1; // the word was phone and not call
                }
            }
            else b = 0;
            if (b != -1)
            {
                while ((b < aux.Length) && ((aux[b] == ' ') || (aux[b] == ':') || (aux[b] == '|') || (aux[b] == ';') || (aux[b] == ',') || (aux[b] == '\n') || (aux[b] == '\t')))
                    b += 1;
                if ((b < aux.Length) && ((aux[b] == '(') || ((aux[b] >= '0') && (aux[b] <= '9'))))
                {
                    string phoneNumber = "";
                    /*                    int e = b;
                                        do
                                        {
                                            if (e != b)
                                                e += 1;
                                            e = aux.IndexOf(" ", e);
                                        } while ((e != -1) && (e + 1 < aux.Length) && ((aux[e + 1] == '(') || (aux[e + 1] == ')') || ((aux[e + 1] >= '0') && (aux[e + 1] <= '9'))));
                                        if (e != -1)
                                        {
                                            int ext = aux.IndexOf("ext", e);
                                            ext += 3;
                                            while ((ext < aux.Length) && ((aux[ext] == ' ') || (aux[ext] == '(') || (aux[ext] == ')') || ((aux[ext] >= '0') && (aux[ext] <= '9'))))
                                                ext += 1;
                                            e = ext;
                                        }
                                        if (e != -1)
                                            dd.data[19] = aux.Substring(b, e - b);
                                        else */
                    int e = aux.IndexOf('|', b);
                    if (e == -1)
                        phoneNumber = aux.Substring(b, aux.Length - b);
                    else
                        phoneNumber = aux.Substring(b, e - b);
                    dd.data[19] = phoneNumber;
                    aux = aux.Replace(dd.data[19], "");
                    RemoveSpaces(ref aux, true);
                }
                else
                {
                    if (b >= aux.Length)
                        phone = true; 
                }
            }
            return aux;
        }

        string getDealerID(string[] p, SqlConnection myConnection2, StreamWriter writer, ref string AtTheEnd, dealer locations)
        {
            Boolean similar;
            short id = 0, bigger = 0;
            int count = 0;
            string temp;

            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommandReader;
                if (p[7] != "")
                {
                    myCommandReader = new SqlCommand("select * from Dealers where Name = @Name", myConnection2);
                    SqlParameter select = new SqlParameter();
                    select.ParameterName = "@Name";
                    select.Value = p[7];
                    myCommandReader.Parameters.Add(select);
                }
                else
                    myCommandReader = new SqlCommand("select * from Dealers where Name is null", myConnection2);

                myReader = myCommandReader.ExecuteReader();
                similar = myReader.HasRows;
                while (myReader.Read())
                {
                    temp = myReader["DealerID"].ToString();
                    id = Convert.ToInt16(temp.Substring(15, temp.Length - 15));
                    if (id > bigger)
                        bigger = id;

                    if ((p[8] == myReader["URL"].ToString()) || ((p[8] == "") && (myReader["URL"] == DBNull.Value)))
                    {
                        count += 1;
                    }

                    if ((p[13] == myReader["FullAddress"].ToString()) || ((p[13] == "") && (myReader["FullAddress"] == DBNull.Value)))
                    {
                        count += 1;
                    }

/*                   if ((p[11] == myReader["Latitude"].ToString()) || ((p[11] == "") && (myReader["Latitude"] == DBNull.Value)))
                    {
                        count += 1;
                    }


                    if ((p[12] == myReader["Longitude"].ToString()) || ((p[12] == "") && (myReader["Longitude"] == DBNull.Value)))
                    {
                        count += 1;
                    }

                    if ((p[14] == myReader["StreetName"].ToString()) || ((p[14] == "") && (myReader["StreetName"] == DBNull.Value)))
                    {
                        count += 1;
                    }

                    if ((p[15] == myReader["City"].ToString()) || ((p[15] == "") && (myReader["City"] == DBNull.Value)))
                    {
                        count += 1;
                    }

                    if ((p[43] == myReader["Province"].ToString()) || ((p[43] == "") && (myReader["Province"] == DBNull.Value)))
                    {
                        count += 1;
                    }

                    if ((p[16] == myReader["PostalCode"].ToString()) || ((p[16] == "") && (myReader["PostalCode"] == DBNull.Value)))
                    {
                        count += 1;
                    }

                    if ((p[17] == myReader["Country"].ToString()) || ((p[17] == "") && (myReader["Country"] == DBNull.Value)))
                    {
                        count += 1;
                    }

                    if ((p[18] == myReader["Map"].ToString()) || ((p[18] == "") && (myReader["Map"] == DBNull.Value)))
                    {
                        count += 1;
                    }

                    if ((p[19] == myReader["Contact"].ToString()) || ((p[19] == "") && (myReader["Contact"] == DBNull.Value)))
                    {
                        count += 1;
                    }*/

                    if (count == 2) 
                    {
                        myReader.Close();
                        return (temp);
                    }

                    count = 0;
                }
                myReader.Close();
                if (similar)
                {
                    Console.WriteLine("WARNING: Check Dealer " + p[7] + ". There are Dealers with the same data (part of)");
                    AtTheEnd = AtTheEnd + "WARNING: Check Dealer " + p[7] + ". There are Dealers with the same data (part of)\n";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            ///// COMECAR A INSERIR LOCATIONS A PARTIR DAQUI. PRECISARA DE OUTRA SENTENCA SQL PARA A TABELA LOCATION

            SqlCommand myCommandDealer = new SqlCommand("INSERT INTO Dealers (DealerID, Name, URL, Latitude, Longitude, FullAddress, StreetName, City, Province, PostalCode, Country, Map, Contact) Values (@DealerID, @Company, @CompanyURL, @Latitude, @Longitude, @FullAddress, @StreetName, @City, @Province, @PostalCode, @Country, @Map, @CompanyPhone)", myConnection2);


            bigger += 1;
            if (bigger > 30000)
            {
                Console.Write("WARNING: To many DealerIDs with almost the same name (the difference is the number)!");
                AtTheEnd = AtTheEnd + "\nWARNING: To many DealerIDs with almost the same name! (the difference is the number)\n\n";
            }
            SqlParameter p41a = new SqlParameter();
            p41a.ParameterName = "@DealerID";
            temp = p[7];
            if (temp.Length > 14)
                temp = temp.Substring(0,14);
            for (int i = temp.Length; i < 15; i++)
                temp += "_";
            string temp1 = temp + bigger.ToString();

            try
            {
                Boolean exist = false;
                do
                {
                    exist = false;
                    SqlDataReader myReader = null;
                    SqlCommand myCommandReader = new SqlCommand("select * from Dealers where DealerID = @ID", myConnection2);

                    SqlParameter select = new SqlParameter();
                    select.ParameterName = "@ID";
                    select.Value = temp1;
                    myCommandReader.Parameters.Add(select);
                    myReader = myCommandReader.ExecuteReader();
                    if (myReader.Read())
                    {
                        exist = true;
                        bigger += 1;
                        temp1 = temp + bigger.ToString();
                    }
                    myReader.Close();
                }
                while (exist);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            p41a.Value = temp1;
            myCommandDealer.Parameters.Add(p41a);

            //Everything is NULL in the dealer
            if ((p[8] == "") && (p[11] == "") && (p[12] == "") && (p[13] == "") && (p[14] == "") && (p[15] == "") && (p[16] == "") && (p[17] == "") && (p[18] == "") && (p[19] == "") && (p[43] == ""))
            {
                Console.WriteLine("WARNING: Check Dealer " + temp1 + ". It is completely NULL\n");
                AtTheEnd = AtTheEnd + "WARNING: Check Dealer " + temp1 + ". It is completely NULL\n";
            }

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@Company";
            if ((p[7] == "") || (p[7] == "{:-("))
                p5.Value = DBNull.Value;
            else
                p5.Value = p[7];
            myCommandDealer.Parameters.Add(p5);

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@CompanyURL";
            if ((p[8] == "") || (p[8] == "{:-("))
                p6.Value = DBNull.Value;
            else
                p6.Value = p[8];
            myCommandDealer.Parameters.Add(p6);

            SqlParameter p9 = new SqlParameter();
            p9.ParameterName = "@Latitude";
            if ((p[11] == "") || (p[11] == "{:-("))
                p9.Value = DBNull.Value;
            else
                p9.Value = p[11];
            myCommandDealer.Parameters.Add(p9);

            SqlParameter p10 = new SqlParameter();
            p10.ParameterName = "@Longitude";
            if ((p[12] == "") || (p[12] == "{:-("))
                p10.Value = DBNull.Value;
            else
                p10.Value = p[12];
            myCommandDealer.Parameters.Add(p10);

            SqlParameter p11 = new SqlParameter();
            p11.ParameterName = "@FullAddress";
            if ((p[13] == "") || (p[13] == "{:-("))
                p11.Value = DBNull.Value;
            else
                p11.Value = p[13];
            myCommandDealer.Parameters.Add(p11);

            SqlParameter p12 = new SqlParameter();
            p12.ParameterName = "@StreetName";
            if ((p[14] == "") || (p[14] == "{:-("))
                p12.Value = DBNull.Value;
            else
                p12.Value = p[14];
            myCommandDealer.Parameters.Add(p12);

            SqlParameter p13 = new SqlParameter();
            p13.ParameterName = "@City";
            if ((p[15] == "") || (p[15] == "{:-("))
                p13.Value = DBNull.Value;
            else
                p13.Value = p[15];
            myCommandDealer.Parameters.Add(p13);

            SqlParameter p14 = new SqlParameter();
            p14.ParameterName = "@PostalCode";
            if ((p[16] == "") || (p[16] == "{:-("))
                p14.Value = DBNull.Value;
            else
                p14.Value = p[16];
            myCommandDealer.Parameters.Add(p14);

            SqlParameter p15 = new SqlParameter();
            p15.ParameterName = "@Country";
            if ((p[17] == "") || (p[17] == "{:-("))
                p15.Value = DBNull.Value;
            else
                p15.Value = p[17];
            myCommandDealer.Parameters.Add(p15);

            SqlParameter p16 = new SqlParameter();
            p16.ParameterName = "@Map";
            if ((p[18] == "") || (p[18] == "{:-("))
                p16.Value = DBNull.Value;
            else
                p16.Value = p[18];
            myCommandDealer.Parameters.Add(p16);

            SqlParameter p17 = new SqlParameter();
            p17.ParameterName = "@CompanyPhone";
            if ((p[19] == "") || (p[19] == "{:-("))
                p17.Value = DBNull.Value;
            else
                p17.Value = p[19];
            myCommandDealer.Parameters.Add(p17);

            SqlParameter p18 = new SqlParameter();
            p18.ParameterName = "@Province";
            if ((p[43] == "") || (p[43] == "{:-("))
                p18.Value = DBNull.Value;
            else
                p18.Value = p[43];
            myCommandDealer.Parameters.Add(p18);

            myCommandDealer.ExecuteNonQuery();
            
            return (temp1);
        }

        private void SeparateLatLong(ref string p, ref string p_2)
        {
            int b = 0;
            while ((b < p.Length) && (p[b] != ','))
                b += 1;
            // go to the following char after ','
            b += 1;
            if (b >= p.Length)
            {
                p = "";
                p_2 = "";
            }
            else
            {
                p_2 = p.Substring(b, p.Length - b);
                p = p.Substring(0, b - 1);
                RemoveSpaces(ref p_2, true);
                RemoveSpaces(ref p, true);
            }
        }

        private bool WebsiteValid(string str, string read)
        {

            string valid = this.SingleDataExtraction(str, read);
            if (valid == "{:-(")
            {
                return (true);
            }
            else
                return (false);
        }

    };
    

    // Main program
    public class Program
    {
        static void Main(string[] args)
        {
  //          string myChoice; // only used to have a pause at the end of the program
            List<Thread> CityThreads = new List<Thread>();
            Tags tag = new Tags();
            List<Tags> ListTags = new List<Tags>();
            
            // insertig "tags" into ListTags to start looking for the cities and extracting the information
            // Hard coded now... It must get from a file later


/*            tag.Website = "dealfind.com/toronto";
            tag.InvalidLink = "?\"404 - page\";@\"found\";#\"Failed\"";
            tag.ListOfCities = "?\"<select name=\\\"newCity\\\">\";?\"<option value=\\\"\";(?\"<option value=\\\"\";@\"\\\">\")";
            tag.DealID = "?\"var DealID =\";@\";\"";
            tag.DealLinkURL = "?\"var AffiliateLinkURL = \\\"\";@\"\\\";\"";
            tag.Category = "";
            tag.Company = "?\"\\\"og:title\\\" content=\\\"\";@\"\\\"/>\"";
            tag.CompanyURL = "?\"\\\"og:url\\\" content=\\\"\";@\"\\\"\"";
            tag.Image = "?\"\\\"og:image\\\" content=\\\"\";@\"\\\"\"";
            tag.Description = "?\"\\\"og:description\\\" content=\\\"\";@\"\\\"/>\";||?\"\var DealName = \\\"\";@\"\\\";\"";
            tag.Lattitude = "?\"\\\"og:latitude\\\" content=\\\"\";@\"\\\"\"";
            tag.Longitude = "?\"\\\"og:longitude\\\" content=\\\"\";@\"\\\"\"";
            tag.CompleteAddress = "";
            tag.StreetName = "?\"\\\"og:street-address\\\" content=\\\"\";@\"\\\" />\" ";
            tag.City = "?\"\\\"og:locality\\\" content=\\\"\";@\"\\\" />\"";
            tag.PostalCode = "?\"\\\"og:postal-code\\\" content=\\\"\";@\"\\\" />\"";
            tag.Country = "?\"\\\"og:country-name\\\" content=\\\"\";@\"\\\" />\"";
            tag.Map = "?\"itemprop=\\\"maps\\\" href=\\\"\";@\"\\\" target=\"";
            tag.CompanyPhone = "";
            tag.RegularPrice = "?\"var RegularPriceHTML = '\";@\"';\"";
            tag.OurPrice = "?\"var OurPriceHTML = '\";@\"';\"";
            tag.Save = "?2\"var YouSaveHTML = '\";@\"'(\"";
            tag.Discount = "?2\"var YouSaveHTML = '\";?\"(\";@\")';\" ";
            tag.PayOutAmount = "?\"Share and get paid\";@\"per\"";
            tag.PayOutLink = "#\"www.DealFind.com/MediaConnectApps/trying";
            tag.SecondsTotal = "?\"var DealSeconds_Total =\";@\";\"";
            tag.SecondsElapsed = "?\"var DealSeconds_Elapsed =\";@\";\"";
            tag.RemainingTime = "";
            tag.ExpiryTime = "";
            tag.MaxNumberOfVouchers = "?\"var MaxNumberOfVouchers =\";@\";\"";
            tag.MinNunberOfVouchers = "?\"var MinNumberOfVouchers =\";@\";\"";
            tag.DealSoldOut = "?\"var DealSoldOut =\";@\";\"";
            tag.DealEnded = "?\"var DealEnded =\";@\";\"";
            tag.DealValid = "";
            tag.PaidVoucherCount = "?\"var PaidVoucherCount =\";@\";\"";
            tag.Highlights = "?\"<h2>Why You Should Go:</h2>\";?\"<ul>\";@\"</ul>\"";
            tag.BuyDetails = "?\"<h2>What You Should Know:</h2>\";@\"<div><a class=\\\"dealInfo\\\" href=\\\"/deal-faq.php\\\">\"";
            tag.DealText = "?\"<div class=\\\"dealText\\\">\";@\"<div class=\\\"divReviews\\\">\"";
            tag.Reviews = "?\"<div class=\"divReviews\"><h2>Reviews</h2>\";@\"<div class=\\\"dealBox\\\"\"";
            tag.RelatedDeals = "";
            tag.SideDeals = "(?\"<div class=\\\"side-deal clearfix\\\">\";?\"<a href=\\\"\";@\"\\\">\") ";  */

            tag = new Tags();
            tag.data[0] = "http://www.dealfind.com/toronto";
            tag.data[1] = "?\"404 - page\";@\"found\" || ?\"<dl class=\\\"subscribe_box\";@\">\"";
            tag.data[2] = "?\"<select name=\\\"newCity\\\">\";?\"<option value=\\\"\";(?\"<option value=\\\"\";@\"\\\">\")";
            tag.data[3] = "(?\"<div class=\\\"side-deal clearfix\\\">\";?\"<a href=\\\"\";@\"\\\">\") ";
            tag.data[4] = "?\"var DealID =\";@\";\"";
            tag.data[5] = "?\"var AffiliateLinkURL = \\\"\";@\"\\\";\"";
            tag.data[6] = "";
            tag.data[7] = "?\"\\\"og:title\\\" content=\\\"\";@\"\\\"/>\"";
            tag.data[8] = "?\"<a itemprop=\\\"url\\\" href=\\\"\";@\"\\\"\";||?\"\\\"og:url\\\" content=\\\"\";@\"\\\"\"";
            tag.data[9] = "?\"\\\"og:image\\\" content=\\\"\";@\"\\\"\"";
            tag.data[10] = "?\"\\\"og:description\\\" content=\\\"\";@\"\\\"/>\";||?\"\var DealName = \\\"\";@\"\\\";\"";
            tag.data[11] = "?\"\\\"og:latitude\\\" content=\\\"\";@\"\\\"\";||?\"&sll=\";@\",\";||?\"&ll=\";@\",\"";
            tag.data[12] = "?\"\\\"og:longitude\\\" content=\\\"\";@\"\\\"\"||?\"&sll=\";?\",\";@\"&\";||?\"&ll=\";?\",\";@\"&\"";
            tag.data[13] = "#\"#2#\";?\"ocations:</strong>\";@\"<div class=\\\"divReviews\\\">\"";
            tag.data[14] = "?\"\\\"og:street-address\\\" content=\\\"\";@\"\\\" />\" ";
            tag.data[15] = "?\"\\\"og:locality\";?\"content=\\\"\"-\"/>\";@\",\"||?\"\\\"og:locality\\\" content=\\\"\";@\"\\\" />\"";
            tag.data[16] = "?\"\\\"og:postal-code\\\" content=\\\"\";@\"\\\" />\"";
            tag.data[17] = "?\"\\\"og:country-name\\\" content=\\\"\";@\"\\\" />\"||?\"<div class=\\\"cities\"-\"var DealName\";?$43;?<\"cities list\";@\"\\\"\"";
            tag.data[18] = "?\"itemprop=\\\"maps\\\" href=\\\"\";@\"\\\" target=\"";
            tag.data[19] = "?\"<div itemprop=\\\"description\";?\"Phone\"-\"/div>\";@\"<\";||?\"<div itemprop=\\\"description\";?\"phone\"-\"/div>\";@\"<\";||?\"<div itemprop=\\\"description\";?\"Email\"-\"/div>\";@\"<\";||?\"<div itemprop=\\\"description\";?\"email\"-\"/div>\";@\"<\";||?\"<div itemprop=\\\"description\";?\"E-mail\"-\"/div>\";@\"<\";||?\"<div itemprop=\\\"description\";?\"e-mail\"-\"/div>\";@\"<\"";
            tag.data[20] = "?2\"var RegularPriceHTML = '\";@\"';\";||?\"var RegularPriceHTML = '\";@\"';\"";
            tag.data[21] = "?2\"var OurPriceHTML = '\";@\"';\";||?\"var OurPriceHTML = '\";@\"';\"";
            tag.data[22] = "?2\"var YouSaveHTML = '\";@\"(\"";
            tag.data[23] = "?2\"var YouSaveHTML = '\";?\"(\";@\")';\"";
            tag.data[24] = "?\"Share and get paid\";@\"per\"";
            tag.data[25] = "";
            tag.data[26] = "?\"var DealSeconds_Total =\";@\";\"";
            tag.data[27] = "?\"var DealSeconds_Elapsed =\";@\";\"";
            tag.data[28] = "";
            tag.data[29] = "#\"#1#\"";
            tag.data[30] = "?\"var MaxNumberOfVouchers =\";@\";\"";
            tag.data[31] = "?\"var MinNumberOfVouchers =\";@\";\"";
            tag.data[32] = "?\"var DealSoldOut =\";@\";\"";
            tag.data[33] = "?\"var DealEnded =\";@\";\"";
            tag.data[34] = "";
            tag.data[35] = "?\"var PaidVoucherCount =\";@\";\"";
            tag.data[36] = "?\"<h2>Why You Should Buy:</h2>\";?\"<ul>\";@\"</ul>\"";
            tag.data[37] = "?\"<h2>What You Should Know:</h2>\";@\"<div><a class=\\\"dealInfo\\\" href=\\\"/deal-faq.php\\\">\"";
            tag.data[38] = "?\"<div class=\\\"dealText\\\">\";@\"<div class=\\\"divReviews\\\">\"";
            tag.data[39] = "?\"<div class=\\\"divReviews\\\"><h2>Reviews</h2>\";@\"<div class=\\\"dealBox\\\"\"";
            tag.data[40] = "";
            tag.data[41] = "?2\"/\""; //find DealID/AlternativeID in weblink
            tag.data[42] = "?\"var AffiliateLinkURL = \\\"\";?4\"/\";@\"\\\";\""; //Alternative ID
            tag.data[43] = "?\"\\\"og:locality\";?\"content=\\\"\"-\"/>\";?\",\";@\"\\\" />\";0;#\"\\|\\|\";?\"<div class=\\\"cities\";?$44;?<\"<h3>\";@\"<\"||?\"\\\"og:locality\";?\"content=\\\"\"-\"/>\";?\",\";@\"\\\" />\"||?\"<div class=\\\"cities\";?$44;?<\"<h3>\";@\"<\"";
            tag.data[44] = "?\"var UserCityName = \\\"\";@\"\\\"\"";
            tag.data[45] = "";
            tag.data[46] = "";
            tag.data[47] = "";
            tag.data[48] = "";
            tag.data[49] = "?\"<title>\";?\"from\";@\"<\"";

            ListTags.Add(tag);

            tag = new Tags();
            tag.data[0] = "http://www.teambuy.ca/toronto";
            tag.data[1] = "?\"Sorry we could not find the page\";?\"<h2>The requested page: \\\"\";@\"\\\" can't be found\"||?\"<h1 style=\\\"text-align:center;\\\">TeamBuyers, we are undergoing a few site updates.\";@\"1>\"";
            tag.data[2] = "(?\"<option value=\\\"\";@\"\\\"\")||?\"<select id='headerCitySub'\";(?\"<option value='\";@\"'\")";
            tag.data[3] = "(?\"<div id=\\\"room\";?\"href=\\\"\";?\".ca\"@\"\\\">\")-\"nearBuysContent\"||(?\"<a class=\\\"image-link\\\" href=\\\"http://www.teambuy.ca\";@\"\\\"\")";
            tag.data[4] = "?\"http://www.teambuy.ca/\";?\"/\";@\"/\\\"\"";
            tag.data[5] = "?\"\\\"og:url\\\" content=\\\"\";@\"\\\" />\"";
            tag.data[6] = "";
            tag.data[7] = "?\"<div id=\\\"companyName\\\">\";@\"</div>\"";
            tag.data[8] = "?\"<div id=\\\"companyWebsite\\\">\";?\"href=\\\"\";@\"\\\"\"";
            tag.data[9] = "?\"\\\"og:image\\\" content=\\\"\";@\"\\\"\";||?\"rel=\\\"image_src\\\" href=\\\"\";@\"\\\"\"";
            tag.data[10] = "?\"\\\"og:title\\\" content=\\\"TeamBuy.ca |\";@\"\\\" />\"";
            tag.data[11] = "?\"http://maps.google.ca/maps?\";?\"&sll=\";@\",\"";
            tag.data[12] = "?\"http://maps.google.ca/maps?\";?\"&sll=\";?\",\";@\"&\"";
            tag.data[13] = "#\"#4#\";?\"Locations:\";@\"<div style=\\\"float:right\\\">\";0;#\";\";?\"http://maps.google.ca/maps\"-\"<!-- GOOGLE MAP -->\";?\"http://maps.google.ca/maps\";?<\"<!-- -->\";(?\"http://maps.google.ca/maps\";@\"\\\"\");||#\"#4#\";?\"Locations:\";@\"<div style=\\\"float:right\\\">\";||#\"#4#\";?\"http://maps.google.ca/maps\"-\"<!-- GOOGLE MAP -->\";?\"http://maps.google.ca/maps\";?<\"<!-- -->\";(?\"http://maps.google.ca/maps\";@\"\\\"\");||#\"#4#\";?\"$(\\\"#more_maps\\\")\";(?\"http://maps.google.ca/maps\";@\"\\\"\")";
            tag.data[14] = "?\"<div id=\\\"companyAddress\\\">\";@\"</div>\"";
            tag.data[15] = "";
            tag.data[16] = "";
            tag.data[17] = "#\"Canada\"";
            tag.data[18] = "?\"http://maps.google.ca/maps?\";?<\"\\\"\";@\"\\\"\"";
            tag.data[19] = "?\"<div id=\\\"companyPhone\\\">\";@\"</div>\"";
            tag.data[20] = "?\"<dt>VALUE:\";?2\">\";@\"</dd>\"";
            tag.data[21] = "?\" <dt>PRICE:\";?2\">\";@\"</dd>\"";
            tag.data[22] = "?\"<dt>SAVE:\";?2\">\";@\"/\"";
            tag.data[23] = "?\"<dt>SAVE:\";?2\">\";?\"/\";@\"</dd>\"";
            tag.data[24] = "?\"Refer friends, get\";@\"</span>\"";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "?\"Time Left To Buy\";?\";\\\">\";@\"<\"";
            tag.data[29] = "#\"#2#\"";
            tag.data[30] = "";
            tag.data[31] = "?\"more needed<br/>\";?<\"\\\">\";@\"more needed<br/>\";0;#\"+\";$35;||?\"more buy needed\";?<\"Just\";@\"more buy needed\";0;#\"+\";$35;||?\"Minimum of\";@\"Reached\";||?\"<div style=\\\"padding-top:5px;display: table-cell;vertical-align: middle;\\\">\";?6\"</span>\";?<2\">\";?2\" \";@\" \"";
            tag.data[32] = "";
            tag.data[33] = "";
            tag.data[34] = "?\"<span id=\\\"btn-buy_soldout\\\">\";@\"<\"";
            tag.data[35] = "?\"<div style=\\\"padding-top:5px;display: table-cell;vertical-align: middle;\\\">\";?\">\";@\"<br />\"";
            tag.data[36] = "?\"div id=\\\"boxMiddleHighlights\\\">\";?\">\";@\"</ul>\"";
            tag.data[37] = "?\"div id=\\\"boxMiddleDetails\\\">\";?\">\";@\"<div id=\\\"boxBottomDetails\\\"></div>\"";
            tag.data[38] = "?\"<div style=\\\"float:left; height:100%; width: 66%\\\">\";@\"<div style=\\\"float:right\\\">\"";
            tag.data[39] = "?\"<div id=\\\"reviewsWide\\\">\";?\"text-align:justify;\\\">\";@\"<div class=\\\"boxBottom710\\\">\"";
            tag.data[40] = "";
            tag.data[41] = "?2\"/\""; //find DealID/AlternativeID in weblink
            tag.data[42] = ""; //Alternative ID
            tag.data[43] = "";
            tag.data[44] = "";
            tag.data[45] = "";
            tag.data[46] = "";
            tag.data[47] = "";
            tag.data[48] = "";
            tag.data[49] = "?\"<title>\";@\"|\"-\"<\";||?\"<title>\";@\"<\"";

            /*          tag.data[0] = "http://www.teambuy.ca/toronto";
                        tag.data[1] = " ";
                        tag.data[2] = " ";
                        tag.data[3] = " ";
                        tag.data[4] = " ";
                        tag.data[5] = " ";
                        tag.data[6] = " ";
                        tag.data[7] = " ";
                        tag.data[8] = " ";
                        tag.data[9] = " ";
                        tag.data[10] = " ";
                        tag.data[11] = " ";
                        tag.data[12] = " ";
                        tag.data[13] = " ";
                        tag.data[14] = " ";
                        tag.data[15] = " ";
                        tag.data[16] = " ";
                        tag.data[17] = " ";
                        tag.data[18] = " ";
                        tag.data[19] = " ";
                        tag.data[20] = " ";
                        tag.data[21] = " ";
                        tag.data[22] = " ";
                        tag.data[23] = " ";
                        tag.data[24] = " ";
                        tag.data[25] = " ";
                        tag.data[26] = " ";
                        tag.data[27] = " ";
                        tag.data[28] = " ";
                        tag.data[29] = " ";
                        tag.data[30] = " ";
                        tag.data[31] = " ";
                        tag.data[32] = " ";
                        tag.data[33] = " ";
                        tag.data[34] = " ";
                        tag.data[35] = " ";
                        tag.data[36] = " ";
                        tag.data[37] = " ";
                        tag.data[38] = " ";
                        tag.data[39] = " ";
                        tag.data[40] = " ";*/
            ListTags.Add(tag);

            tag = new Tags();
            tag.data[0] = "http://www.wagjag.com/toronto";
            tag.data[1] = "?\"<title>WagJag - Never Miss a WagJag!\";@\">\"";
            tag.data[2] = "(?\"class=\\\"city_link\\\" href=\\\"\";?\">\";@\"<\")";
            tag.data[3] = "(?\"<span><strong>&rsaquo;<a href=\\\"\";@\"\\\">\")";
            tag.data[4] = "?\"js_wagjag_id\\\" value=\\\"\";@\"\\\"\"";
            tag.data[5] = "?\"id=\\\"deal_headline\\\"><a href=\\\"\";@\"\\\">\"";
            tag.data[6] = "";
            tag.data[7] = "?\"<!-- <h1>About \";@\"</h1>\"";
            tag.data[8] = "?\"<br /><a href=\\\"\";@\"\\\"\";||?\"from <a href=\\\"\";@\"\\\"\";||?\"at <a href=\\\"\";@\"\\\"\";||?\"at<a href=\\\"\";@\"\\\"\";||?\"service on <a href=\\\"\"@\"\\\"\";||?\"target=\\\"_blank\\\">site\";?<\"<a href=\\\"\";@\"\\\"\"";
            tag.data[9] = "?\"\\\"og:image\\\" content=\\\"\";@\"\\\"\"";
            tag.data[10] = "?\"<meta name=\\\"description\\\" content=\\\"\";@\"\\\"/>\"";
            tag.data[11] = "";
            tag.data[12] = "";
            tag.data[13] = "#\"#3#\";?\"var sites = [[\";?<\"var\";@\"]]\"||#\"#3#\";?\"<strong>Locations\";?\"</p>\";@\"<strong>Reviews\";||#\"#3#\";?\"<strong>Locations\";?\"</p>\";@\"<br /><a href=\\\"\";||#\"#3#\";?\"<!-- End Deal Information -->\";?<\"<strong>Reviews\";?<\"</p>\";@\"<strong>Reviews\"||#\"#3#\";?\"<!-- End Deal Information -->\";?<\"<br /><a href=\\\"\";?<\"<div>\";?\"</p>\n\";@\"<br /><a href=\\\"\"||#\"#3#\";?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";?\"</li></ul>\";@\"<br /><a href=\\\"\"";
//            tag.data[13] = "?\"var sites = [[\";?<\"var\";@\"]]\"||?\"<strong>Locations\";?\"</p>\";@\"<strong>Reviews\";||?\"<strong>Locations\";?\"</p>\";@\"<br /><a href=\\\"\";||?\"<!-- End Deal Information -->\";?<\"<strong>Reviews\";?<\"</p>\";@\"<strong>Reviews\"||?\"<!-- End Deal Information -->\";?<\"<br /><a href=\\\"\";?<\"<div>\";?\"</p>\n\";@\"<br /><a href=\\\"\"||?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";?\"</li></ul>\";@\"<br /><a href=\\\"\"||?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";@\"<br /><a href=\\\"\"";
            tag.data[14] = "";
            tag.data[15] = "";
            tag.data[16] = "";
            tag.data[17] = "";
            tag.data[18] = "";
            tag.data[19] = "";
            tag.data[20] = "?\"<td>Regular Price:</td><td>\";@\"</td>\"";
            tag.data[21] = "?\">Buy For\";@\"<\"";
            tag.data[22] = "?\"<td>You Save:</td><td>\";@\"</td>\"";
            tag.data[23] = "?\"<td>Discount:</td><td>\";@\"</td>\"";
            tag.data[24] = "";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "";
            tag.data[29] = "#\"#3#\";?\"TargetDate = \\\"\";@\"\\\";\"";
            tag.data[30] = "";
            tag.data[31] = "?\"class=\\\"deal_activated\\\">\";?\" at\";@\"<\"";
            tag.data[32] = "?\"buy_btn\\\" ><a href=\\\"#\\\"\";@\"<\"";
            tag.data[33] = "";
            tag.data[34] = "?\"buy_btn\\\" ><a href=\\\"#\\\"\";@\"<\"";
            tag.data[35] = "?\"id=\\\"amount_bought\\\">\";?2\">\";@\"<\"";
            tag.data[36] = "?\"class=\\\"deal_highlights\\\">\";?\"<ul>\";@\"</ul>\"";
            tag.data[37] = "?\"class=\\\"offer_details\\\"\";?\"<ul>\";@\"</ul>\"";
            tag.data[38] = "?\"id=\\\"deal_information\\\"\";?\"<div>\";@\"<strong>Locations\";||?\"id=\\\"deal_information\\\"\";?\"<div>\";@\"<strong>Reviews\";||?\"id=\\\"deal_information\\\"\";?\"<div>\";@\"/><a href=\"";
            tag.data[39] = "?\"Reviews</strong>\";@\"<br /><a href=\"";
            tag.data[40] = "#\"www.wagjag.com/\";?\"class=\\\"related_wagjag\\\"\";?\"<a href=\\\"\";@\"\\\">\"";
            tag.data[41] = "?\"=\""; //find DealID/AlternativeID in weblink
            tag.data[42] = ""; //Alternative ID
            tag.data[43] = "";
            tag.data[44] = "";
            tag.data[45] = "";
            tag.data[46] = "";
            tag.data[47] = "";
            tag.data[48] = "?\"<div id=\\\"exp_container\";?\"</div>\"-\"<!-- end header_content -->\";(?\"<a href=\\\"\";@\"\\\"\")";
            tag.data[49] = "?\"<title>\";@\"-\"";
            ListTags.Add(tag);

            tag = new Tags();
            tag.data[0] = "http://www.wagjag.com/toronto/?vertical=grocery";
            tag.data[1] = "?\"<title>WagJag - Never Miss a WagJag!\";@\">\"";
            tag.data[2] = "#\"toronto\"";
            tag.data[3] = "(?\"<span><strong>&rsaquo;<a href=\\\"?\";@\"\\\">\")";
            tag.data[4] = "?\"js_wagjag_id\\\" value=\\\"\";@\"\\\"\"";
            tag.data[5] = "?\"id=\\\"deal_headline\\\"><a href=\\\"\";@\"\\\">\"";
            tag.data[6] = "#\"Grocery\"";
            tag.data[7] = "?\"<!-- <h1>About \";@\"</h1>\"";
            tag.data[8] = "?\"<br /><a href=\\\"\";@\"\\\"\";||?\"from <a href=\\\"\";@\"\\\"\";||?\"at <a href=\\\"\";@\"\\\"\";||?\"at<a href=\\\"\";@\"\\\"\";||?\"target=\\\"_blank\\\">site\";?<\"<a href=\\\"\";@\"\\\"\"";
            tag.data[9] = "?\"\\\"og:image\\\" content=\\\"\";@\"\\\"\"";
            tag.data[10] = "?\"<meta name=\\\"description\\\" content=\\\"\";@\"\\\"/>\"";
            tag.data[11] = "";
            tag.data[12] = "";
            tag.data[13] = "#\"#3#\";?\"var sites = [[\";?<\"var\";@\"]]\"||#\"#3#\";?\"<strong>Locations\";?\"</p>\";@\"<strong>Reviews\";||#\"#3#\";?\"<strong>Locations\";?\"</p>\";@\"<br /><a href=\\\"\";||#\"#3#\";?\"<!-- End Deal Information -->\";?<\"<strong>Reviews\";?<\"</p>\";@\"<strong>Reviews\"||#\"#3#\";?\"<!-- End Deal Information -->\";?<\"<br /><a href=\\\"\";?<\"<div>\";?\"</p>\n\";@\"<br /><a href=\\\"\"||#\"#3#\";?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";?\"</li></ul>\";@\"<br /><a href=\\\"\"";
            tag.data[14] = "";
            tag.data[15] = "";
            tag.data[16] = "";
            tag.data[17] = "#\"Canada\"";
            tag.data[18] = "";
            tag.data[19] = "";
            tag.data[20] = "?\"<td>Regular Price:</td><td>\";@\"</td>\"";
            tag.data[21] = "?\">Buy For\";@\"<\"";
            tag.data[22] = "?\"<td>You Save:</td><td>\";@\"</td>\"";
            tag.data[23] = "?\"<td>Discount:</td><td>\";@\"</td>\"";
            tag.data[24] = "";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "";
            tag.data[29] = "#\"#3#\";?\"TargetDate = \\\"\";@\"\\\";\"";
            tag.data[30] = "";
            tag.data[31] = "?\"class=\\\"deal_activated\\\">\";?\" at\";@\"<\"";
            tag.data[32] = "?\"buy_btn\\\" ><a href=\\\"#\\\"\";@\"<\"";
            tag.data[33] = "";
            tag.data[34] = "?\"buy_btn\\\" ><a href=\\\"#\\\"\";@\"<\"";
            tag.data[35] = "?\"id=\\\"amount_bought\\\">\";?2\">\";@\"<\"";
            tag.data[36] = "?\"class=\\\"deal_highlights\\\">\";?\"<ul>\";@\"</ul>\"";
            tag.data[37] = "?\"class=\\\"offer_details\\\"\";?\"<ul>\";@\"</ul>\"";
            tag.data[38] = "?\"id=\\\"deal_information\\\"\";?\"<div>\";@\"<strong>Locations\";||?\"id=\\\"deal_information\\\"\";?\"<div>\";@\"<strong>Reviews\";||?\"id=\\\"deal_information\\\"\";?\"<div>\";@\"/><a href=\"";
            tag.data[39] = "?\"Reviews</strong>\";@\"<br /><a href=\"";
            tag.data[40] = "#\"www.wagjag.com/\";?\"class=\\\"related_wagjag\\\"\";?\"<a href=\\\"\";@\"\\\">\"";
            tag.data[41] = "?\"=\""; //find DealID/AlternativeID in weblink
            tag.data[42] = ""; //Alternative ID
            tag.data[43] = "";
            tag.data[44] = "";
            tag.data[45] = "";
            tag.data[46] = "";
            tag.data[47] = "";
            tag.data[48] = "";
            tag.data[49] = "?\"<title>\";@\"-\"";
            ListTags.Add(tag);

            tag = new Tags();
            tag.data[0] = "http://www.livingsocial.com/cities/";
            tag.data[1] = "?\"<title>Missing Deal - LivingSocial\";@\">\"";
            tag.data[2] = "?\"<a href=\\\"/cities/\";(?\"<a href=\\\"/cities/\";@\"\\\"\")";
            tag.data[3] = "(?\"<a class=\\\"price\\\" href=\\\"/cities/\";@\"\\\"\")";
            tag.data[4] = "?\"property=\\\"og:url\\\" content=\\\"http://\";?2\"/\";@\"-\"";
            tag.data[5] = "?\"property=\\\"og:url\\\" content=\\\"\";@\"\\\"/>\"";
            tag.data[6] = "?\"class=\\\"title header \";@\"\\\"\"";
            tag.data[7] = "?\"<div class=\\\"deal-title\\\">\";?\"<h1>\";@\"</h1>\"";
            tag.data[8] = "?\" at <a target=\\\"_blank\\\" href=\\\"\"-\"#sfwt_short_1').show\";@\"\\\"\";||?\" at <a href=\\\"\"-\"#sfwt_short_1').show\";@\"\\\"\";||?\" from <a target=\\\"_blank\\\" href=\\\"\"-\"#sfwt_short_1').show\";@\"\\\"\";||?\" from <a href=\\\"\"-\"#sfwt_short_1').show\";@\"\\\"\";||?\" toward <a target=\\\"_blank\\\" href=\\\"\"-\"#sfwt_short_1').show\";@\"\\\"\";||?\" toward <a href=\\\"\"-\"#sfwt_short_1').show\";@\"\\\"\";||?\" deal. <a href=\\\"\"-\"#sfwt_short_1').show\";@\"\\\"\";||?\"<div id=\\\"sfwt_short_1\\\"><p>\"-\"#sfwt_short_1').show\";?\"href=\\\"http\";?<\"\\\"\";@\"\\\"\"||?\"<div id=\\\"sfwt_short_1\\\"><p>\"-\"#sfwt_short_1').show\";?\"href=\\\"www\";?<\"\\\"\";@\"\\\"\"";
            tag.data[9] = "?\"property=\\\"og:image\\\" content=\\\"\";@\"\\\"/>\"";
            tag.data[10] = "?\"<div class=\\\"deal-title\\\">\";?\"<p>\";@\"</p>\"";
            tag.data[11] = "?\"http://maps.google.com/maps?q=\";@\"\\\"\"";
            tag.data[12] = "";
            tag.data[13] = "#\"#5#\";(?\"<span class=\\\"street_1\\\">\";@\"<span class=\\\"directions\\\">\"),\"\\|\"";
            tag.data[14] = "";
            tag.data[15] = "";
            tag.data[16] = "";
            tag.data[17] = "?\"<div class=\\\"all-cities\\\">\";?$44;?<\"<h2>\";@\"<\"";
            tag.data[18] = "?\"class=\\\"directions\\\"><a href=\\\"\";@\"\\\"\"";
            tag.data[19] = "";
            tag.data[20] = "";
            tag.data[21] = "?\"<div class=\\\"deal-price \\\">\";?\"</sup>\";@\"</div>\";||?\"<div class=\\\"deal-price \\\">\";@\"<\"   ";
            tag.data[22] = "";
            tag.data[23] = "?\"id=\\\"percentage\\\">\";@\"<\"";
            tag.data[24] = "";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "?\"class=\\\"last\\\">\"-\"sfwt_short_1\";?\">\";@\"</div>\"";
            tag.data[29] = "#\"#2#\"";
            tag.data[30] = "";
            tag.data[31] = "";
            tag.data[32] = "";
            tag.data[33] = "";
            tag.data[34] = "?\"deal-over\";@\"\\\"\";||?\"class=\\\"deal-buy-box buy-now-over\";@\"\\\">\";||?\"class=\\\"deal-buy-box buy-now-soldout\";@\"\\\">\"";
            tag.data[35] = "?\"class=\\\"purchased\\\">\";?\">\";@\"<\"";
            tag.data[36] = "";
            tag.data[37] = "?\"class=\\\"fine-print\\\">\";?\"<p>\";@\"</p>\"";
            tag.data[38] = "?\"<div id=\\\"sfwt_short_1\\\"><p>\";@\"</p> <a class=\\\"sfwt\\\"\"";
            tag.data[39] = "";
            tag.data[40] = "";
            tag.data[41] = "?2\"/\";@\"-\""; //find DealID/AlternativeID in weblink
            tag.data[42] = ""; //Alternative ID
            tag.data[43] = "";
            tag.data[44] = "?\"title=\\\"LivingSocial -\";@\"\\\"\"";
            tag.data[45] = "";
            tag.data[46] = "";
            tag.data[47] = "";
            tag.data[48] = "";
            tag.data[49] = "?\"<meta name=\\\"keywords\\\" content=\\\"\";@\",\"";
            ListTags.Add(tag);

            tag = new Tags();
            tag.data[0] = "http://www.dealticker.com/product.php?city=Calgary";
            //tag.data[0] = "http://www.dealticker.com/toronto_en_1categ.html";
            tag.data[1] = "?\"no_deal=true\";@\";\" || ?\"404 Not Found\";@\">\"";
            tag.data[2] = "?\"<div id=\\\"city_list\\\"\";(?\"<a href=\\\"http://www.dealticker.com\";@\"\\\"\")-\"<ul class=\\\"lavaLampWithImage\\\" id=\\\"1\\\">\"";
            tag.data[3] = "?2\"<!-- Today's Side Deal -->\";(?\"<!-- Today's Side Deal -->\";?\"<a href=\\\"\";?3\"/\";@\"\\\"\")-\"<td valign=\\\"bottom\\\" style=\\\"height: 170px;\\\">\"";
            tag.data[4] = "?\"http://www.dealticker.com/product.php/tab/1/product_id/\";@\"\\\"\"";
            tag.data[5] = "?\"<div class=\\\"short_description\\\"><a href=\\\"\"@\"\\\"\"";
            tag.data[6] = "";
            tag.data[7] = "?\"class=\\\"fine_print\\\">Location\";?\"<tr><td><b>\";@\"</b>\"||?\"class=\\\"fine_print\\\">Location\";?\"<b>\";@\"</b>\";||?\"<div id=\\\"description\\\"\";?\"from <a href=\"-\"class=\\\"value_name\";?\">\";@\"<\"";
            tag.data[8] = "?\"<div class=\\\"fine_print\\\">Location\";?\"<a href=\\\"http:\"-\"</div></div>\";?<\"\\\"\";@\"\\\"\"";
            tag.data[9] = "?\"<img id='product_img' src='\";@\"'\"";
            tag.data[10] = "?\"name=\\\"description\\\" content=\\\"\";@\"\\\"\"";
            tag.data[11] = "?\"google.maps.LatLng(\";@\",\"";
            tag.data[12] = "?\"google.maps.LatLng(\";?\",\";@\")\"";
//            tag.data[13] = "#\"#1#\";?\"class=\\\"fine_print\\\">Location\";(?\"</b><br>\";@\"</div>\");0;#\";\";?\"<div id=\\\"location_description\\\"\";?\"<p>\";@\"</div\"||#\"#1#\";?\"class=\\\"fine_print\\\">Location\";(?\"</b><br>\";@\"</div>\");||#\"#1#\";?\"class=\\\"fine_print\\\">Location\";(?\"</b></td></tr><tr><td>\";@\"</div>\");0;#\";\";?\"<div id=\\\"location_description\\\"><p>\";@\"</div\";||#\"#1#\";?\"class=\\\"fine_print\\\">Location\";(?\"</b></td></tr><tr><td>\";@\"</div>\");||#\"#1#\";?\"<div id=\\\"location_description\\\"><p>\";@\"</div\"";
            tag.data[13] = "#\"#1#\";?\"<div id=\\\"location_description\\\"><p>\";@\"<\";||#\"#1#\";?\"class=\\\"fine_print\\\">Location\";(?\"</b><br>\";@\"</div>\")||#\"#1#\";?\"class=\\\"fine_print\\\">Location\";(?\"</b></td></tr><tr><td>\";@\"</div>\")";
            tag.data[14] = "";
            tag.data[15] = "?\"class=\\\"fine_print\\\">Location\";?\"</b></td></tr><tr><td>\";@\"<\"||?\"class=\\\"fine_print\\\">Location\";?\"</b><br>\";@\",\"-\"<div style\"";
//            tag.data[15] = "";
            tag.data[16] = "";
//            tag.data[14] = "?\"class=\\\"fine_print\\\">Location\";?2\"</td></tr><tr><td>\";@\"</\"||?\"class=\\\"fine_print\\\">Location\";?\"</b><br>\";?\",\"-\"</div>\";@\",\"";
//            tag.data[16] = "?\"class=\\\"fine_print\\\">Location\";?3\"</td></tr><tr><td>\";@\"</\"||?\"class=\\\"fine_print\\\">Location\";?\"</b><br>\";?2\",\"-\"</div>\";@\",\"";
            tag.data[17] = "?$44;?\"<span>\";@\"<\"";
            tag.data[18] = "";
            tag.data[19] = "";
//            tag.data[19] = "?\"class=\\\"fine_print\\\">Location\";?4\"</td></tr><tr><td>\";@\"</\"||?\"class=\\\"fine_print\\\">Location\";?\"</b><br>\";?3\",\"-\"</div>\";@\"</div>\"";
            tag.data[20] = "?\"class=\\\"value\\\">$\";@\"<\"";
            tag.data[21] = "?\"class=\\\"price\\\"\";?\"$\";@\"<\"";
            tag.data[22] = "?2\"class=\\\"value\\\">$\";@\"<\"";
            tag.data[23] = "?2\"class=\\\"value\\\">\";@\"%<\"";
            tag.data[24] = "?\"Share Today's Deal and get $\";@\" \"";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "?\"Time Remaining:\";?2\"<tr>\";@\"</tr>\"";
            tag.data[29] = "#\"#2#\"";
            tag.data[30] = "?\"var qty_max = \";@\";\"";
            tag.data[31] = "?\"var qty_min = \";@\";\"";
            tag.data[32] = "";
            tag.data[33] = "";
            tag.data[34] = "?\"img src=\\\"http://www.dealticker.com/images/button-expired\";@\"\\\"\"";
            tag.data[35] = "?\"class=\\\"deal_bought\\\">\"@\"b\"";
            tag.data[36] = "?\"class=\\\"highlight\\\">\";?\"<ul>\";@\"</ul>\"";
            tag.data[37] = "?\"<div id='fine_print'\";?\"<ul>\";@\"</div>\"";
            tag.data[38] = "?\"<div id=\\\"description\\\"\";?\"style=\\\"font-size: small;\\\">\";@\"</span></p></div>\"";
            tag.data[39] = "";
            tag.data[40] = "";
            tag.data[41] = "?2\"/\""; //find DealID/AlternativeID in weblink
            tag.data[42] = ""; //Alternative ID
            tag.data[43] = "?\"<div id=\\\"city_list\"-\"http://www.dealticker.com/user/register.php\";?$15;?<\"<div>\";@\"<\";||?\"<div id=\\\"city_list\"-\"http://www.dealticker.com/user/register.php\";?$45;?<\"<div>\";@\"<\"";
            tag.data[44] = "?$43;?<\"<div id=\\\"\";@\"\\\"\"";
            tag.data[45] = "?\"city=\";@\"\\\"\"";
            tag.data[46] = "";
            tag.data[47] = "";
            tag.data[48] = "";
            tag.data[49] = "?\"<title>\";@\":\"";
            ListTags.Add(tag);

/*            tag = new Tags();
            tag.data[0] = "$http://http://www.dealticker.com/get-xml-feed.php?user=16465";
            tag.data[1] = "";
            tag.data[2] = "";
            tag.data[3] = "";
            tag.data[4] = " ";
            tag.data[5] = " ";
            tag.data[6] = " ";
            tag.data[7] = " ";
            tag.data[8] = " ";
            tag.data[9] = " ";
            tag.data[10] = " ";
            tag.data[11] = " ";
            tag.data[12] = " ";
            tag.data[13] = " ";
            tag.data[14] = " ";
            tag.data[15] = " ";
            tag.data[16] = " ";
            tag.data[17] = " ";
            tag.data[18] = " ";
            tag.data[19] = " ";
            tag.data[20] = " ";
            tag.data[21] = " ";
            tag.data[22] = " ";
            tag.data[23] = " ";
            tag.data[24] = " ";
            tag.data[25] = " ";
            tag.data[26] = " ";
            tag.data[27] = " ";
            tag.data[28] = " ";
            tag.data[29] = " ";
            tag.data[30] = " ";
            tag.data[31] = " ";
            tag.data[32] = " ";
            tag.data[33] = " ";
            tag.data[34] = " ";
            tag.data[35] = " ";
            tag.data[36] = " ";
            tag.data[37] = " ";
            tag.data[38] = " ";
            tag.data[39] = " ";
            tag.data[40] = " ";
            ListTags.Add(tag);
*/

            // end of hard coding tags

            List<string> baseaddress = new List<string>();
            baseaddress.Add("http://www.dealfind.com/$");
            baseaddress.Add("http://www.teambuy.ca/$");
            baseaddress.Add("http://www.wagjag.com/$");
            baseaddress.Add("http://www.wagjag.com/?$&vertical=grocery");
            baseaddress.Add("http://www.livingsocial.com/cities/$");
            baseaddress.Add("http://www.dealticker.com/$");

            List<string> DontHandleFirstPage = new List<string>();
            DontHandleFirstPage.Add("http://www.teambuy.ca/toronto");


            // Transfer Timed Out Deals to DealsEnded Table
            SqlConnection myConnection = new SqlConnection("server=MEDIACONNECT-PC\\MCAPPS; Trusted_Connection=yes; database=Deals; connection timeout=15");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                myConnection = new SqlConnection("server=FIVEFINGERFINDS\\MEDIACONNECT; Trusted_Connection=yes; database=Deals; connection timeout=15");
                try
                {
                    myConnection.Open();
                }
                catch (Exception error)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine(error.ToString());
                }
            }
            DateTimeOffset current_time = DateTimeOffset.Now;
            using (SqlCommand myCommand = new SqlCommand("MERGE Deals.dbo.DealsEnded USING (SELECT * FROM [Deals].[dbo].[DealsList] where ExpiryTime < @CURRENT_TIME) as source on (Deals.dbo.DealsEnded.DealID = source.dealID) WHEN not matched then insert (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, Saved, Discount, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews, DealSite) Values (source.Website, source.DealID, source.DealLinkURL, source.Category, source.Image, source.Description, source.DealerID, source.RegularPrice, source.OurPrice, source.Saved, source.Discount, source.PayOutAmount, source.PayOutLink, source.ExpiryTime, source.MaxNumberVouchers, source.MinNumberVouchers, source.PaidVoucherCount, source.DealExtractedTime, source.Highlights, source.BuyDetails, source.DealText, source.Reviews, source.DealSite);", myConnection))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@CURRENT_TIME";
                param.Value = current_time;
                myCommand.Parameters.Add(param);

                myCommand.ExecuteNonQuery();
            }
            using (SqlCommand myCommand = new SqlCommand("DELETE FROM [Deals].[dbo].[DealsList] where ExpiryTime < @CURRENT_TIME", myConnection))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@CURRENT_TIME";
                param.Value = current_time;
                myCommand.Parameters.Add(param);

                myCommand.ExecuteNonQuery();
            }
            myConnection.Close();



  //          for (int i = 0; i < ListTags.Count; i++)
            {
                int i = 5;
                string website = ListTags.ElementAt(i).data[0];
                if ((website.Length > 6) && (website.Substring(0, 6) != "$STOP$"))
                {
                    Extraction site = new Extraction(ListTags.ElementAt(i), baseaddress.ElementAt(i), DontHandleFirstPage);
                    //                string website = ListTags.ElementAt(i).data[0];
                    //                CityExtraction site = new CityExtraction(ListTags.ElementAt(i));
                    Thread t = new Thread(new ThreadStart(site.ExtractingCities));
                    t.Name = website;
                    //                CityThreads.Add(t);
                    t.Start();
                }
            }
/*            while (CityThreads.Count() != 0)
            {
                for (int i = 0; i < CityThreads.Count(); i++)
                {
                    if (!CityThreads.ElementAt(i).IsAlive)
                    {
                        Console.WriteLine("removing " + CityThreads.ElementAt(i).Name);
                        CityThreads.RemoveAt(i);
                        Console.WriteLine(CityThreads.Count());
                    }
                }
            }*/

            // Transfer Timed Out Deals to DealsEnded Table again (normally half an hour after the previous transfer)
            myConnection = new SqlConnection("server=MEDIACONNECT-PC\\MCAPPS; Trusted_Connection=yes; database=Deals; connection timeout=15");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                myConnection = new SqlConnection("server=FIVEFINGERFINDS\\MEDIACONNECT; Trusted_Connection=yes; database=Deals; connection timeout=15");
                try
                {
                    myConnection.Open();
                }
                catch (Exception error)
                {
                    Console.WriteLine(e.ToString());
                    Console.WriteLine(error.ToString());
                }
            }
            current_time = DateTimeOffset.Now;
            using (SqlCommand myCommand = new SqlCommand("MERGE Deals.dbo.DealsEnded USING (SELECT * FROM [Deals].[dbo].[DealsList] where ExpiryTime < @CURRENT_TIME) as source on (Deals.dbo.DealsEnded.DealID = source.dealID) WHEN not matched then insert (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, Saved, Discount, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews, DealSite) Values (source.Website, source.DealID, source.DealLinkURL, source.Category, source.Image, source.Description, source.DealerID, source.RegularPrice, source.OurPrice, source.Saved, source.Discount, source.PayOutAmount, source.PayOutLink, source.ExpiryTime, source.MaxNumberVouchers, source.MinNumberVouchers, source.PaidVoucherCount, source.DealExtractedTime, source.Highlights, source.BuyDetails, source.DealText, source.Reviews, source.DealSite);", myConnection))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@CURRENT_TIME";
                param.Value = current_time;
                myCommand.Parameters.Add(param);

                myCommand.ExecuteNonQuery();
            }
            using (SqlCommand myCommand = new SqlCommand("DELETE FROM [Deals].[dbo].[DealsList] where ExpiryTime < @CURRENT_TIME", myConnection))
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@CURRENT_TIME";
                param.Value = current_time;
                myCommand.Parameters.Add(param);

                myCommand.ExecuteNonQuery();
            }
            myConnection.Close();
            
//            myChoice = Console.ReadLine();
        }
    }
}
