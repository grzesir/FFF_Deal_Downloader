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
using System.Xml;
using System.Collections.Concurrent;
namespace PlayingWithCsharp
{
    public class location
    {
        string contact = "";
        string postalCode = "";
        public string streetAddress = "";
        public string city = "";
        public string province = "";
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
        public dealer sortByProvinceCityAndStreetAddress(dealer l)
        {
            List<location> temp = l.Locations;
            temp = temp.OrderBy(loc => loc.province).ThenBy(loc => loc.city).ThenBy(loc => loc.streetAddress).ToList();
            l.Locations = temp;
            return (l);
        }
        public location getLocation(int i)
        {
            location loc = Locations[i];
            return loc;
        }
        public location GetRemoveLocation(int i)
        {
            location loc = Locations[i];
            Locations.RemoveAt(i);
            return loc;
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
        List<deals> ListOfEvDeals = new List<deals>();

        public int addList(dealslist input, List<Tags> listOfDeals)
        {
            int qty = input.CountDeals();
            int repeated = 0;
            for (int i = 0; i < qty; i++)
            {
                deals temp = input.GetDealDetails(i);
                string dealid = temp.GetDealID();
                string altDealid = temp.GetAlternativeID();
                int j;
                for (j = 0; j < this.ListOfEvDeals.Count; j++)
                {
                    if (this.ListOfEvDeals[j].GetDealID() == dealid)
                        break;
//                    if (this.ListOfEvDeals[j].GetAlternativeID() == altDealid)
//                        break;
                }
                if (j < this.ListOfEvDeals.Count)
                {
                    int cont = 0;
                    for (int ind = 0; ind < listOfDeals.Count; ind++)
                    {
                        if (listOfDeals.ElementAt(ind).data[4] == dealid)
                        {
                            if (cont > 0)
                                listOfDeals.RemoveAt(ind);
                            cont += 1;
                        }
                    }
                    repeated += (cont - 1);
                    List<string> cities = temp.GetListCities();
                    for (int ind = 0; ind < cities.Count; ind++)
                        this.ListOfEvDeals[j].AddCity(cities[ind]);
                }
                else
                    this.ListOfEvDeals.Add(temp);
            }
            return repeated;
        }
        public int DealEvaluated(string ID)
        {
            for (int i = 0; i < this.ListOfEvDeals.Count; i++)
            {
                if (this.ListOfEvDeals[i].GetAlternativeID() == ID)  // First this one. By default, alternativeID is "", so different from ID.
                    return (i);
                if (this.ListOfEvDeals[i].GetDealID() == ID)
                    return (i);
            }
            return (-1);
        }
        public void AddCity(int i, string city)
        {
            this.ListOfEvDeals[i].AddCity(city);
        }
        public void SetDealID(string ID, string alternative, string city)
        {
            deals newdeal = new deals();
            newdeal.SetDealID(ID);
            newdeal.SetAlternativeID(alternative);
            newdeal.AddCity(city);
            ListOfEvDeals.Add(newdeal);
        }
        public deals GetDealDetails(int i)
        {
            return ListOfEvDeals[i];
        }
        public int CountDeals()
        {
            return (ListOfEvDeals.Count());
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

        List<string> listOfCities;
        StreamWriter writer;
//        ConcurrentQueue<Tags> QueueOfDeals;
        List<Tags> listOfDeals;
        dealslist listOfEvaluatedDeals;
        string sourceLocations;
        List<string> extraData;
        List<string> messages;
        int pos;

        public Extraction(Tags oneWebsite, string baseAddress, List<string> DontHandleFirstPage)
        {
            this.oneWebsite = oneWebsite;
            this.baseAddress = baseAddress;
            this.DontHandleFirstPage = DontHandleFirstPage;
        }

        public Extraction(List<string> listOfCities, StreamWriter writer, List<Tags> listOfDeals, dealslist listOfEvaluatedDeals, string sourceLocations, List<string> extraData, Tags oneWebsite, string baseAddress, List<string> DontHandleFirstPage, List<string> message, int pos)
        {
            this.listOfCities = listOfCities;
            this.writer = writer;
            this.listOfDeals = listOfDeals;
            this.listOfEvaluatedDeals = listOfEvaluatedDeals;
            this.sourceLocations = sourceLocations;
            this.extraData = extraData;
            this.oneWebsite = oneWebsite;
            this.baseAddress = baseAddress;
            this.DontHandleFirstPage = DontHandleFirstPage;
            this.messages = message;
            this.pos = pos;
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
                if ((DealData.data[num] != "") && (k.GetTimes() == 0)) k.SetTimes(1);
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
                    k.SetTimes(10 * k.GetTimes() + (str[c1] - 48));
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
                return ("");
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
                        return ("");
                    }
                } while (str[c2 - 1] == '\\');
                str_new = str.Substring(c1, c2 - c1);
                c1 = c2 + 1;
            }
            else
            {
                Console.WriteLine("Error in #Constant tag format. Probably missing \" at " + c1 + " character.");
                AtTheEnd += "ERROR: Error in #Constant tag format. Probably missing \" at " + c1 + " character.";
                return ("");
            }
            str_new = str_new.Replace("\\|","|");
            str_new = str_new.Replace("\\\"", "\"");
            return (str_new);
        }

        public string GetRepeatedOperation(string str, ref int c1, ref string read)
        {
            string str_new = "";
            if (c1 > str.Length)
            {
                Console.WriteLine("Mistake at the end of ( keyword.");
                AtTheEnd += "ERROR: Mistake at the end of ( keyword.";
                return ("");
            }
            int c2 = c1 - 1;
            do
            {
                c2 = str.IndexOf(')', c2 + 1);
                if (c2 == -1)
                {
                    Console.WriteLine("Missing ). Can't go on.");
                    AtTheEnd += "ERROR: Missing ). Can't go on.";
                    return ("");
                }
            } while (str[c2 - 1] == '\\');
            str_new = str.Substring(c1, c2 - c1);
            c2 += 1;

            if ((c2 < str.Length) && (str[c2] == '-'))
            {
                c2 += 1;
                keywords aux = GetEndString(str, ref c2);
                int aux_pos = 0;
                while ((aux.GetTimes() > 0) && (aux_pos != -1))
                {
                    aux.SetTimes(aux.GetTimes() - 1);
                    aux_pos = read.IndexOf(aux.GetKeyword(), aux_pos);
                    //                    if (aux_pos == -1)
                    //                    {
                    //                        Console.WriteLine("The data you are looking for can not be found in the HTML file");
                    //                        return("");
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
            int last_valid_pos = position; // used only if there is an || or 0 inside () (optional searchs inside repetition)
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
            string temp_rep_read = read;
            string separator = "\n";
//            string constValue = "";
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
                        read = temp_rep_read;
                        last_valid_pos = read_pos;
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
                    }
                }

                else if (str[c] == '|')
                {
//                    if (((c + 1) < str.Length) && (str[c + 1] == '|') && (!op_rep))
                    if (((c + 1) < str.Length) && (str[c + 1] == '|'))
                    {
                        if (err_not_found != "")
                            result = "";
                        else
                        {
                            RemoveTags(ref result);
                            if (result!="")
                                if ((result.Length <= 4) && (result[0] == '#') && (result[result.Length - 1] == '#'))
                                    result = "";
                            if (result == "http://")
                                result = "";
                            if (result != "")
                            {
                                c = str.Length;
                                continue;
                            }
                        }
                        if (op_rep)
                        {
                            read_pos = last_valid_pos;
                            read = temp_rep_read;
                        }
                        else
                        {
                            read_pos = pos_ini;
                            read = temp_read;
                        }
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
                        return ("");
                    }
                }

                // getting the operations that must be executed continuasly till the end of the html page
                else if (str[c] == '(')
                {
                    string r;
                    c += 1;
                    r = GetRepeatedOperation(str, ref c, ref read);

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
                                byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(search.GetKeyword());
                                string normal = System.Text.Encoding.UTF8.GetString(tempBytes);
                                if (normal != search.GetKeyword())
                                {
                                    read_pos = saved_pos;
                                    search.SetKeyword(normal);
                                    continue;
                                }
                                else
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
                                    //                                return(""); //ends the Thread?
                                }
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
                                byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(search.GetKeyword());
                                string normal = System.Text.Encoding.UTF8.GetString(tempBytes);
                                if (normal != search.GetKeyword())
                                {
                                    read_pos = saved_pos;
                                    search.SetKeyword(normal);
                                    continue;
                                }
                                else
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
                                    //                                return(""); //ends the Thread?
                                }
                            }
                            else
                                if (search.GetTimes() > 1)
                                    read_pos -= 1;
                                else
                                    read_pos += search.GetKeyword().Length;
                        }
                        byte[] tempBytes1 = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(search.GetKeyword());
                        string normal1 = System.Text.Encoding.UTF8.GetString(tempBytes1);
                        if ((normal1 == search.GetKeyword()) || (read_pos != -1))
                            search.SetTimes(search.GetTimes() - 1);
                        else
                        {
                            read_pos = saved_pos;
                            search.SetKeyword(normal1);
                        }
                    }

                    if (read_pos != -1)
                    {

                        temp_rep_read = read;
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
                        last_valid_pos = -1;
                    }
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
                    if (op_rep)
                    {
                        read_pos = last_valid_pos;
                        read = temp_rep_read;
                    }
                    else
                    {
                        read_pos = pos_ini;
                        read = temp_read;
                    }
                    c += 1;
                    continue;
                }
                // getting the constant string that must be added to the information we are looking for
                else if (str[c] == '#')
                {
                    string r;
                    c += 1;
                    r = GetConstant(str, ref c);
                    if (r == "")
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
                                byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(search.GetKeyword());
                                string normal = System.Text.Encoding.UTF8.GetString(tempBytes);
                                if (normal != search.GetKeyword())
                                {
                                    read_pos = saved_pos;
                                    search.SetKeyword(normal);
                                    continue;
                                }
                                else
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
                                    //                                return (""); //ends the Thread?
                                }
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
                                byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(search.GetKeyword());
                                string normal = System.Text.Encoding.UTF8.GetString(tempBytes);
                                if (normal != search.GetKeyword())
                                {
                                    read_pos = saved_pos;
                                    search.SetKeyword(normal);
                                    continue;
                                }
                                else
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
                                    //                                return(""); //ends the Thread?
                                }
                            }
                            else
                                if (search.GetTimes() > 1)
                                    read_pos -= 1;
                                else
                                    read_pos += search.GetKeyword().Length;
                        }
                        byte[] tempBytes1 = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(search.GetKeyword());
                        string normal1 = System.Text.Encoding.UTF8.GetString(tempBytes1);
                        if ((normal1 == search.GetKeyword()) || (read_pos != -1))
                            search.SetTimes(search.GetTimes() - 1);
                        else
                        {
                            read_pos = saved_pos;
                            search.SetKeyword(normal1);
                        }
                    }
                    if (last_valid_pos == -1)
                        last_valid_pos = read_pos;
                    if ((c >= str.Length) || (read_pos == -1))
                        continue;
                    c += 1;
                    search = new keywords();
                    search = GetSearchString(str, ref c, DealData, read);
                    if ((search.GetKeyword() == "") && (search.GetTimes() == 0))
                    {
                        err_not_found = "The search string (result of $ operation) is empty";
                    }
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
                                byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(search.GetKeyword());
                                string normal = System.Text.Encoding.UTF8.GetString(tempBytes);
                                if (normal != search.GetKeyword())
                                {
                                    read_pos = saved_pos;
                                    search.SetKeyword(normal);
                                    continue;
                                }
                                else
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
                                    //                                return(""); //ends the Thread?
                                }
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
                                byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(search.GetKeyword());
                                string normal = System.Text.Encoding.UTF8.GetString(tempBytes);
                                if (normal != search.GetKeyword())
                                {
                                    read_pos = saved_pos;
                                    search.SetKeyword(normal);
                                    continue;
                                }
                                else
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
                                    //                                return(""); //ends the Thread?
                                }
                            }
                            else
                                if (search.GetTimes() > 1)
                                    read_pos -= 1;
                                else
                                    read_pos += search.GetKeyword().Length;
                        }
                        byte[] tempBytes1 = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(search.GetKeyword());
                        string normal1 = System.Text.Encoding.UTF8.GetString(tempBytes1);
                        if ((normal1 == search.GetKeyword()) || (read_pos != -1))
                            search.SetTimes(search.GetTimes() - 1);
                        else
                        {
                            read_pos = saved_pos;
                            search.SetKeyword(normal1);
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
                            //                            return(""); //ends the Thread?
                        }
                        end_pos += end.GetKeyword().Length;
                    }
                    if (end_pos != -1)
                    {
                        string text;
                        end_pos -= end.GetKeyword().Length;
                        text = read.Substring(read_pos, end_pos - read_pos);
                        RemoveSpaces(ref text, false, false);
                        result = result + text;
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
                    return ("");
                }
            }
            if (err_not_found != "")
                return("");
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
            result = result.Replace("<u>", "");
            result = result.Replace("</u>", "");
            result = result.Replace("<tr>", "");
            result = result.Replace("</tr>", "\n");
            result = result.Replace("<center>", "\n");
            result = result.Replace("</center>", "\n");
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
            htmlTag = "<h";
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
            htmlTag = "</h";
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
            htmlTag = "<img";
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

            RemoveSpaces(ref result, false, false);
        }

        private void RemoveAmpersand(ref string result)
        {
            result = result.Replace("&amp;", "&");
            result = result.Replace("&#039;", "'");
            result = result.Replace("&rsquo;", "’");
            result = result.Replace("&nbsp;", "");
            result = result.Replace("&quot;", "\"");
            result = result.Replace("&ldquo;", "\"");
            result = result.Replace("&rdquo;", "\"");
            result = result.Replace("&rsquo;", "'");
            result = result.Replace("&lsquo;", "'");
            result = result.Replace("&lt;", "<");
            result = result.Replace("&gt;", ">");
            result = result.Replace("&tilde;", "~");
            RemoveSpaces(ref result, false, false);
        }

        
        private void RemoveSpaces(ref string temp_result, Boolean ponctuation, Boolean commas) // Ponctuation indicates if it has to remove ponctuation characters. It must be true only when handling the data and false when extracting from webpages
        {
            int b = 0;
            int e = temp_result.Length - 1;
            if ((e == 0) && (temp_result[0] != ' ') && (temp_result[0] != '\n') && (temp_result[0] != '\t') && (temp_result[b] != 160) && (temp_result[b] != '\r') && (temp_result[b] != '|') && (!isPunctuation(temp_result[b], commas)))
                return;
            if (ponctuation)
            {
                while ((b <= e) && ((temp_result[b] == ' ') || (temp_result[b] == 160) || (temp_result[b] == '\n') || (temp_result[b] == '\t') || (temp_result[b] == '\r') || (temp_result[b] == '|') || (isPunctuation(temp_result[b], commas))))
                {
                    b += 1;
                }
                while ((e > b) && ((temp_result[e] == ' ') || (temp_result[e] == 160) || (temp_result[e] == '\n') || (temp_result[e] == '\t') || (temp_result[e] == '\r') || (temp_result[e] == '|') || (isPunctuation(temp_result[e], commas)) || (temp_result[e] == '-') || (temp_result[e] == '&')))
                {
                    e -= 1;
                }
            }
            else
            {
                while ((b <= e) && ((temp_result[b] == ' ') || (temp_result[b] == 160) || (temp_result[b] == '\n') || (temp_result[b] == '\t') || (temp_result[b] == '\r') || (temp_result[b] == '|')))
                {
                    b += 1;
                }
                while ((e > b) && ((temp_result[e] == ' ') || (temp_result[e] == 160) || (temp_result[e] == '\n') || (temp_result[e] == '\t') || (temp_result[e] == '\r') || (temp_result[e] == '|') || (temp_result[e] == '-') || (temp_result[e] == '&')))
                {
                    e -= 1;
                }
            }
            if (e >= b)
                temp_result = temp_result.Substring(b, (e + 1) - b);
            else
                temp_result = "";
        }

        private bool isPunctuation(char p, Boolean commas)
        {
            if ((p == '.') || (p == ';') || (p == ':') || (p == '?') || (p == '!'))
                return true;
            if ((commas) && (p == ','))
                return true;
            return false;
        }

        string DownloadData(string URL)
        {
/*            // Set a default policy level for the "http:" and "https" schemes.
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
            return strReturn; */

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
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
                Console.WriteLine("ERROR: Exception reading from webpage " + URL + "\n");
                strData = "ERROR: Exception reading from webpage";
            }
            return strData;
        }

        // Thread responsible for extracting the all of the cities links for a given website
        public void ExtractingCities()
        {
            StreamWriter writer;
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


            int numThreads = 1;

            if (this.oneWebsite.data[0].IndexOf(';') != -1)
            {
                int pos = this.oneWebsite.data[0].IndexOf(';');
                numThreads = Convert.ToInt16(this.oneWebsite.data[0].Substring(0,pos));
                this.oneWebsite.data[0] = this.oneWebsite.data[0].Substring(pos + 1);
            }

            read = DownloadData(this.oneWebsite.data[0]);

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
//            string extraData[];
            string[] parts;
//            List<Tags> listOfDeals = new List<Tags>();
//            ConcurrentQueue<Tags> QueueOfDeals = new ConcurrentQueue<Tags>();

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
            if (temp == "")
            {
                Console.WriteLine("ERROR: Couldn't find cities in website " + this.oneWebsite.data[0]);
                writer.WriteLine("ERROR: Couldn't find cities in website " + this.oneWebsite.data[0]);
                writer.Close();
                return;
            }
            string sourceLocations = read;
            parts = temp.Split('\n');

            string total_extraData = "";
            dealslist total_listOfEvaluatedDeals = new dealslist();
            List<Tags> totalListOfDeals = new List<Tags>();



            if (numThreads == 1)
            {
                List<string> listOfCities = new List<string>();
                List<string> extraData = new List<string>();
                List<string> messages = new List<string>();
                extraData.Add("");
                messages.Add("");

                for (int i = 0; i < parts.Length; i++)
                {
                    if ((parts[i] != "") && (!listOfCities.Contains(parts[i])))
                    {
                        listOfCities.Add(parts[i]);
                    }
                }
                writer.WriteLine("Website | xx | ListOfCities | yy | DealID | DealLinkURL | Category | Company | CompanysURL | Image | Description | Latitude | Longitude | CompleteAddress | StreetName | City | PostalCode | Country | Map | CompanysPhone | RegularPrice | OurPrice | Save | Discount | PayOutAmount | PayOutLink | SecondsTotal | SecondsElapsed | RemainingTime | ExpiryTime | MaxNumberOfVouchers | MinNumberOfVouchers | DealSoldOut | DealEnded | DealValid | PaidVoucherCount | Highlights | BuyDetails | DealText | Reviews | RelatedDeals (same company)");


                ManualResetEvent resetEvent = new ManualResetEvent(false);
                //                int toProcess = numThreads;

                Extraction citiesDeals = new Extraction(listOfCities, writer, totalListOfDeals, total_listOfEvaluatedDeals, sourceLocations, extraData, this.oneWebsite, this.baseAddress, this.DontHandleFirstPage, messages, 0);
                Thread t = new Thread(new ThreadStart(citiesDeals.ExtractingDeals));
                t.Name = this.oneWebsite.data[0];
                t.Start();

                t.Join();

                total_extraData = extraData[0];
                this.AtTheEnd = this.AtTheEnd + messages[0];
            }
            else
            {
//                int i = parts.Length;
//                numThreads = (i / 50) + 1;

                List<string>[] listOfCities = new List<string>[numThreads];
                //            listOfCities[numThreads] = new List<string>();
                List<string> extraData = new List<string>();
                List<string> messages = new List<string>();
                dealslist[] listOfEvaluatedDeals = new dealslist[numThreads];
                List<Tags>[] listOfDeals = new List<Tags>[numThreads];
                int j = 0;
                int max = (parts.Length / numThreads) + 1;

                for (int i = 0; i < numThreads; i++)
                {
                    listOfCities[i] = new List<string>();
                    extraData.Add("");
                    messages.Add("");
                    listOfEvaluatedDeals[i] = new dealslist();
                    listOfDeals[i] = new List<Tags>();
                }

                for (int i = 0; i < parts.Length; i++)
                {
                    if (i >= max + (j * max))
                        j += 1;

                    if ((parts[i] != "") && (!listOfCities[j].Contains(parts[i])))
                    {
                        listOfCities[j].Add(parts[i]);
                    }
                }
                writer.WriteLine("Website | xx | ListOfCities | yy | DealID | DealLinkURL | Category | Company | CompanysURL | Image | Description | Latitude | Longitude | CompleteAddress | StreetName | City | PostalCode | Country | Map | CompanysPhone | RegularPrice | OurPrice | Save | Discount | PayOutAmount | PayOutLink | SecondsTotal | SecondsElapsed | RemainingTime | ExpiryTime | MaxNumberOfVouchers | MinNumberOfVouchers | DealSoldOut | DealEnded | DealValid | PaidVoucherCount | Highlights | BuyDetails | DealText | Reviews | RelatedDeals (same company)");


                ManualResetEvent resetEvent = new ManualResetEvent(false);
                int toProcess = numThreads;
                Thread[] t = new Thread[numThreads];

                for (j = 0; j < numThreads; j++)
                {

                    Extraction citiesDeals = new Extraction(listOfCities[j], writer, listOfDeals[j], listOfEvaluatedDeals[j], sourceLocations, extraData, this.oneWebsite, this.baseAddress, this.DontHandleFirstPage, messages, j);
                    //                string website = ListTags.ElementAt(i).data[0];
                    //                CityExtraction site = new CityExtraction(ListTags.ElementAt(i));
                    t[j] = new Thread(new ThreadStart(citiesDeals.ExtractingDeals));
                    t[j].Name = this.oneWebsite.data[0] + j;
                    //                CityThreads.Add(t);
                    t[j].Start();
                }

                for (j = 0; j < numThreads; j++)
                    t[j].Join();

                // verificar como pegar extradata de varias threads e juntar num unico string
                // passar listOfEvaluatedDeals como um array, sendo um indice para cada thread. Desta forma, cada um nao sera concorrente e sera possivel juntar depois
                // Fazer o mesmo com extraData. ListOfDeals sera um concurrentBag ou concurrentQueue? Verificar!

                // tratar extradata e juntar listOfEvaluatedDeals

                //          writer.WriteLine("Total of cities: " + listOfCities.Count + "\n\n\n");

                for (j = 0; j < numThreads; j++)
                {
                    totalListOfDeals.AddRange(listOfDeals[j]);
                }
                int repeated = 0;
                for (j = 0; j < numThreads; j++)
                {
                    total_extraData = total_extraData + extraData[j];
                    repeated += total_listOfEvaluatedDeals.addList(listOfEvaluatedDeals[j], totalListOfDeals);
                    this.AtTheEnd = this.AtTheEnd + messages[j];
                }
                Console.WriteLine(repeated + " repeated deals!\n\n");
                this.AtTheEnd = this.AtTheEnd + repeated + " repeated deals!\n\n";
            }


            handlingStoringData(writer, totalListOfDeals, total_listOfEvaluatedDeals, total_extraData);
        
        }

        public void ExtractingDeals()
        {
            // listOFDeals - tem somente um ADD, que eh light-weight

            // listOfEvaluatedDeals - Usa bastante. Nao precisa ser compartilhado mas precisa retornar e ser comparado com os outros. Se ja existir o Deal, apenas inclui-se as cidades. Senao, inclui-se tudo.
            // extraData - tem que ser retornado e tratado em seguida. 

        //    string extraData = "";
            string read;
            string temp;

            string str;
            string[] parts;
//            dealslist listOfEvaluatedDeals = new dealslist();
   //         List<Tags> listOfDeals = new List<Tags>();

            List<string> TryLater = new List<string>();
//            for (int we = 0; we < 1; we ++)
//            {   string item = "abc";
            foreach (string item in listOfCities)
            {
                List<string> SideDeals = new List<string>();
                List<string> EvaluatedSideDeals = new List<string>();
 //               List<string> SpecialDeals = new List<string>();
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

//                    URL = "http://www.giltcity.com/miami/flyingtrapezemiami";
                    if ((part_URL.Length >= 7) && (part_URL.Substring(0, 7) == "http://"))
                        URL = part_URL;
                    else
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
                            messages[pos] = messages[pos] + "WARNING: Invalid website: " + URL + "\n";
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
                            if (relatedDeals != "")
                                if (temp != "")
                                    temp = temp + relatedDeals;
                                else
                                    temp = relatedDeals;
//                            if (false)
                            if (temp != "")
                            {
                                List<string> tempSideDeals;
                                parts = temp.Split('\n');
                                tempSideDeals = new List<string>(parts);
                                for (int ind = 0; ind < tempSideDeals.Count; ind++)
                                {
                                    string s_withData = "";
                                    string s = tempSideDeals[ind];
                                    int begin = s.IndexOf("&$");
                                    if (begin != -1)
                                    {
                                        s_withData = baseAddress.Replace("$", s) + "|";
                                        s = s.Remove(begin);
                                    }
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
                                                {
                                                    SideDeals.Add(s);
                                                }
                                            }
                                        }
                                    }
                                    if (s_withData != "")
                                    {
                                        if (!extraData[pos].Contains(s))
                                        {
                                            extraData[pos] = extraData[pos] + s_withData;
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
                                if ((temp != "") && (temp != ""))
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
                                if (DealID == "")
                                {
                                    Console.WriteLine("WARNING: Couldn't find the DealID in website " + URL);
                                    writer.WriteLine("WARNING: Couldn't find the DealID in website " + URL);
                                    messages[pos] = messages[pos] + "WARNING: Couldn't find the DealID in website " + URL + "\n";
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
//                                            if ((j == 20) || (j == 21) || (j == 23))
//                                            {
//                                                Console.Write("");
//                                            }
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
                                        if (DealData.data[17] == "")
                                        {
                                            str = this.oneWebsite.data[17];
                                            RecursList.Add(17);
                                            str = this.SingleDataExtraction(str, sourceLocations, DealData);
                                            RecursList.Remove(17);
                                            DealData.data[17] = str;
                                        }
                                        i = extraData[pos].IndexOf(DealData.data[5]);
                                        if (i != -1)
                                        {
                                            int end = extraData[pos].IndexOf("|", i);
//                                            if (end == -1)   that will never happen, because | is always inserted at the end of each string.
//                                                end = extraData.Length;
                                            string str_aux = extraData[pos].Substring(i, end - i);
                                            extraData[pos] = extraData[pos].Remove(i, end - i + 1);
                                            i = str_aux.IndexOf("&$");
                                            while (str_aux != "")
                                            {
                                                int num;
                                                i += 2;
                                                end = str_aux.IndexOf(':', i);
                                                try
                                                {
                                                    num = Convert.ToInt16(str_aux.Substring(i, end - i));
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("Index from ?$ rule must be a number.'" + str_aux + " Dealsite: " + DealData.data[49] + "\n");
                                                    messages[pos] = messages[pos] + "Index from ?$ rule must be a number.'" + str_aux + " Dealsite: " + DealData.data[49] + "\n";
                                                    break;
                                                }
                                                catch (OverflowException)
                                                {
                                                    Console.WriteLine("Index from the ?$ rule" + str_aux + "' is outside the range of an Int. Dealsite: " + DealData.data[49] + "\n");
                                                    messages[pos] = messages[pos] + "Index from the ?$ rule" + str_aux + "' is outside the range of an Int. Dealsite: " + DealData.data[49] + "\n";
                                                    break;
                                                }
                                                i = end + 1;
                                                end = str_aux.IndexOf(';', i);
                                                if (DealData.data[num] == "")
                                                {
                                                    DealData.data[num] = str_aux.Substring(i, end - i);
                                                }
                                                str_aux = str_aux.Remove(0, end + 1);
                                                i = str_aux.IndexOf("&$");
                                            }
                                        }
                                        this.listOfDeals.Add(DealData);
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
                                messages[pos] = messages[pos] + "ERROR: Giving up link: " + TryItem + "\n";
                            }
                            TryLater = new List<string>();
                        }
                    }
//SPECIAL                } while ((SideDeals.Count() > 0) || (hasSpecialdeals));
                } while (SideDeals.Count() > 0);
            }
            Console.Write("");
        }



        private void handlingStoringData(StreamWriter writer, List<Tags> listOfDeals, dealslist listOfEvaluatedDeals, string extraData)
        {

            int timesCalled = 0;
            int googleMapsCalled = 0;
            SqlConnection myConnection;

//            Console.WriteLine("\n\nNow listing cities with the same deal:");
            writer.WriteLine("\n\n\n\nNow listing cities with the same deal:");
            for (int i = 0; i < listOfEvaluatedDeals.CountDeals(); i++)
            {
                deals Dealdetails = listOfEvaluatedDeals.GetDealDetails(i);
                Tags dealData = new Tags();
                string ID = Dealdetails.GetDealID();
//                Console.Write("\n" + ID + " - ");
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
//                    Console.Write(item + "  ");
                    writer.Write(item + "  ");
                }
            }
//            Console.WriteLine("\n\nTotal of deals: " + listOfEvaluatedDeals.CountDeals());
//            Console.WriteLine("Total of cities: " + listOfCities.Count);
//            Console.WriteLine();
            writer.WriteLine("\n\n\n\nTotal of deals: " + listOfEvaluatedDeals.CountDeals());

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

            List<string> removeText = new List<string>();
            removeText.Add("products shipped to you");
            removeText.Add("valid locations");
            removeText.Add("include photo");
            removeText.Add("(map)");
            removeText.Add("get directions");
            removeText.Add("(carte)");
            removeText.Add("see map");
            removeText.Add("for more information about this package");
            removeText.Add("phone and contact with:");
            removeText.Add("domocilio conocido");
            removeText.Add("on location shoot");
            removeText.Add("call to order");
            removeText.Add("to place your order");
            removeText.Add("once purchased");
            removeText.Add("mailed to your door");
            removeText.Add("see website for directions");
            removeText.Add("click website link to");
            removeText.Add("to redeem voucher,");
            removeText.Add("to redeem voucher");
            removeText.Add("to redeem your voucher,");
            removeText.Add("to redeem your voucher");
            removeText.Add("to book your appointment");
            removeText.Add("include photo");
            removeText.Add("mailing address and contact number");
            removeText.Add("please visit:");
            removeText.Add("please visit");
            removeText.Add("they come to you");
            removeText.Add("for reservations");
            removeText.Add("redeem online by clicking the \"redemption\" link on your voucher");
            removeText.Add("Redeem online by clicking \"Redemption\" link on your voucher");
            removeText.Add("online redemption:");
            removeText.Add("online redemption");
            removeText.Add("web redemption:");
            removeText.Add("web redemption");
            removeText.Add("or redeem");
            removeText.Add("redeem");
            removeText.Add("online at");
            removeText.Add("online:");
            removeText.Add("online");
            removeText.Add("or by phone:");
            removeText.Add("or by phone");
            removeText.Add("by phone:");
            removeText.Add("by phone");
            removeText.Add("mobile service");
            removeText.Add("mobile service:");
            removeText.Add("gta mobile");
//            removeText.Add("mobile");  //is handled later... There is an American city called Mobile
            removeText.Add("call/email");
            removeText.Add("or by email:");
            removeText.Add("or by e-mail:");
            removeText.Add("or by email");
            removeText.Add("or by e-mail");
            removeText.Add("by emailing:");
            removeText.Add("by emailing");
            removeText.Add("by email:");
            removeText.Add("by email");
            removeText.Add("by e-mail:");
            removeText.Add("by e-mail");
            removeText.Add("for inquiries,");
            removeText.Add("for inquiries");
            removeText.Add("please call:");
            removeText.Add("please call");
            removeText.Add("please");
            removeText.Add("call:");
            removeText.Add("or email:");
            removeText.Add("or email");
            removeText.Add("email:");
            removeText.Add("email");
            removeText.Add("or e-mail:");
            removeText.Add("or e-mail");
            removeText.Add("e-mail:");
            removeText.Add("e-mail");
            removeText.Add("multiple locations");
            removeText.Add("valid at");
            removeText.Add("view locations");
            removeText.Add("mail out");


// Store the data into SQL Database. Clean and handle the data, if needed
//            foreach (Tags dd in listOfDeals)
            for (int eachDeal = 0; eachDeal < listOfDeals.Count; eachDeal++)
            {
                Tags dd = listOfDeals.ElementAt(eachDeal);
                string line = "";
                dealer locations = new dealer();
                int i;

                RemoveSpaces(ref dd.data[21], true, true);

                i = dd.data[21].IndexOf(";&;");
                if (i != -1)
                {
                    List<string>[] auxList = new List<string>[50];

                    auxList[21] = new List<string>();

                    i = dd.data[21].LastIndexOf(";&;");
                    while (i != -1)
                    {
                        auxList[21].Add(dd.data[21].Substring((i + 3), dd.data[21].Length - (i + 3)));
                        dd.data[21] = dd.data[21].Remove(i);
                        i = dd.data[21].LastIndexOf(";&;");
                    };

                    int NumberOfDeals = auxList[21].Count;
                    Boolean okay = true;

                    for (int ind = 0; ind < 50; ind++)
                    {
                        if (ind == 21)
                            ind = 22;

                        RemoveSpaces(ref dd.data[ind], true, true);

                        auxList[ind] = new List<string>();

                        i = dd.data[ind].LastIndexOf(";&;");
                        while (i != -1)
                        {
                            auxList[ind].Add(dd.data[ind].Substring((i + 3), dd.data[ind].Length - (i + 3)));
                            dd.data[ind] = dd.data[ind].Remove(i);
                            i = dd.data[ind].LastIndexOf(";&;");
                        }

                        if ((ind != 22) && (auxList[ind].Count > 0) && (auxList[ind].Count != NumberOfDeals))
                        {
                            Console.WriteLine("ERROR: Need more data to divide the Deal into specific deals. DealLink: " + dd.data[5] + ". This deal was discarded.\n");
                            AtTheEnd = AtTheEnd + "ERROR: Need more data to divide the Deal into specific deals. DealLink: " + dd.data[5] + ". This deal was discarded.\n";
                            okay = false;
                            break;
                        }
                    }

                    if (!okay)
                    {
                        continue;
                    }

                    if (NumberOfDeals == auxList[20].Count)
                    {
                        if (auxList[22].Count != NumberOfDeals)
                        {
                            dd.data[22] = "";
                            auxList[22] = new List<string>();
                        }
                    }

                    if (auxList[4].Count == 0)
                    {
                        for (i = 0; i < NumberOfDeals; i++)
                        {
                            auxList[4].Add(dd.data[4] + "_" + i);
                        }
                    }

                    for (i = 0; i < NumberOfDeals; i++)
                    {
                        Tags DealData = new Tags();

                        for (int ind = 0; ind < 50; ind++)
                        {
                            if (auxList[ind].Count > 0)
                                DealData.data[ind] = auxList[ind].ElementAt(i);
                            else
                                DealData.data[ind] = dd.data[ind];
                        }
                        listOfDeals.Add(DealData);
                    }

/*
                    List<string> DealIds = new List<string>();      // dd.data[4]
                    List<string> Descriptions = new List<string>(); // dd.data[10]
                    List<string> RegPrices = new List<string>();    // dd.data[20]
                    List<string> OurPrices = new List<string>();    // dd.data[21]
                    List<string> Savings = new List<string>();      // dd.data[22]
                    List<string> Discounts = new List<string>();    // dd.data[23]
                    List<string> PaidVouchers = new List<string>(); // dd.data[35]
                    RemoveSpaces(ref dd.data[4], true, true);
                    RemoveSpaces(ref dd.data[10], true, true);
                    RemoveSpaces(ref dd.data[20], true, true);
                    RemoveSpaces(ref dd.data[21], true, true);
                    RemoveSpaces(ref dd.data[22], true, true);
                    RemoveSpaces(ref dd.data[23], true, true);
                    RemoveSpaces(ref dd.data[35], true, true);
                    
                    i = dd.data[21].LastIndexOf(";&;");
                    while (i != -1)
                    {
                        OurPrices.Add(dd.data[21].Substring((i + 3),dd.data[21].Length - (i + 3)));
                        dd.data[21] = dd.data[21].Remove(i);
                        i = dd.data[21].LastIndexOf(";&;");
                    };

                    i = dd.data[22].LastIndexOf(";&;");
                    while (i != -1)
                    {
                        Savings.Add(dd.data[22].Substring((i + 3), dd.data[22].Length - (i + 3)));
                        dd.data[22] = dd.data[22].Remove(i);
                        i = dd.data[22].LastIndexOf(";&;");
                    };

                    i = dd.data[23].LastIndexOf(";&;");
                    while (i != -1)
                    {
                        Discounts.Add(dd.data[23].Substring((i + 3), dd.data[23].Length - (i + 3)));
                        dd.data[23] = dd.data[23].Remove(i);
                        i = dd.data[23].LastIndexOf(";&;");
                    };

                    i = dd.data[20].LastIndexOf(";&;");
                    while (i != -1)
                    {
                        RegPrices.Add(dd.data[20].Substring((i + 3), dd.data[20].Length - (i + 3)));
                        dd.data[20] = dd.data[20].Remove(i);
                        i = dd.data[20].LastIndexOf(";&;");
                    };

                    i = dd.data[35].LastIndexOf(";&;");
                    while (i != -1)
                    {
                        PaidVouchers.Add(dd.data[35].Substring((i + 3), dd.data[35].Length - (i + 3)));
                        dd.data[35] = dd.data[35].Remove(i);
                        i = dd.data[35].LastIndexOf(";&;");
                    };

                    i = dd.data[10].LastIndexOf(";&;");
                    while (i != -1)
                    {
                        Descriptions.Add(dd.data[10].Substring((i + 3), dd.data[10].Length - (i + 3)));
                        dd.data[10] = dd.data[10].Remove(i);
                        i = dd.data[10].LastIndexOf(";&;");
                    };

                    i = dd.data[4].LastIndexOf(";&;");
                    while (i != -1)
                    {
                        DealIds.Add(dd.data[4].Substring((i + 3), dd.data[4].Length - (i + 3)));
                        dd.data[4] = dd.data[4].Remove(i);
                        i = dd.data[4].LastIndexOf(";&;");
                    };

                    int NumberOfDeals = OurPrices.Count;

                    if (NumberOfDeals == RegPrices.Count)
                    {
                        if (Savings.Count != NumberOfDeals)
                        {
                            dd.data[22] = "";
                            Savings = new List<string>();
                        }
                    }
                    
                    if ((NumberOfDeals == Descriptions.Count) &&
                       ((NumberOfDeals == PaidVouchers.Count) || (dd.data[35] == "")) &&
                       ((NumberOfDeals == RegPrices.Count) || (dd.data[20] == "")) &&
                       ((NumberOfDeals == Discounts.Count) || (dd.data[23] == "")) &&
                       ((NumberOfDeals == Savings.Count) || (dd.data[22] == "")) &&
                       ((NumberOfDeals == Discounts.Count) || (NumberOfDeals == Savings.Count)) &&
                       ((NumberOfDeals == DealIds.Count) || (DealIds.Count == 0)))
                    {
                        if (DealIds.Count == 0)
                        {
                            for (i = 0; i < NumberOfDeals; i++)
                            {
                                DealIds.Add(dd.data[4] + "_" + i);
                            }
                        }
                        for (i = 0; i < NumberOfDeals; i++)
                        {
                            Tags DealData = new Tags();

                            for (int ind = 0; ind < 50; ind++)
                            {
                                DealData.data[ind] = dd.data[ind];
                            }

                            DealData.data[4] = DealIds.ElementAt(i);
                            DealData.data[10] = Descriptions.ElementAt(i);
                            DealData.data[21] = OurPrices.ElementAt(i);
                            if (RegPrices.Count > 0)
                                DealData.data[20] = RegPrices.ElementAt(i);
                            if (Savings.Count > 0)
                                DealData.data[22] = Savings.ElementAt(i);
                            if (Discounts.Count > 0)
                                DealData.data[23] = Discounts.ElementAt(i);
                            if (PaidVouchers.Count > 0)
                                DealData.data[35] = PaidVouchers.ElementAt(i);
                            listOfDeals.Add(DealData);
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Need more data (Description, OurPrice, RegPrice, DealID, PaidVouchers and (Discounts or Savings)) to divide the Deal into specific deals. DealLink: " + dd.data[5] + ". This deal was discarded.\n");
                        AtTheEnd = AtTheEnd + "ERROR: Need more data (Description, OurPrice, RegPrice, DealID, PaidVouchers and (Discounts or Savings)) to divide the Deal into specific deals. DealLink: " + dd.data[5] + ". This deal was discarded.\n";
                        continue;
                    }
                    */

                }


                // assigning the extra data got from SideDeals to the correct deals and attributes
                i = extraData.IndexOf(dd.data[5]);
                if (i != -1)
                {
                    int end = extraData.IndexOf("|", i);
                    //                                            if (end == -1)   that will never happen, because | is always inserted at the end of each string.
                    //                                                end = extraData.Length;
                    string str_aux = extraData.Substring(i, end - i);
                    extraData = extraData.Remove(i, end - i + 1);
                    i = str_aux.IndexOf("&$");
                    while (str_aux != "")
                    {
                        int num;
                        i += 2;
                        end = str_aux.IndexOf(':', i);
                        try
                        {
                            num = Convert.ToInt16(str_aux.Substring(i, end - i));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Index from ?$ rule must be a number.'" + str_aux + " Dealsite: " + dd.data[49] + "\n");
                            AtTheEnd = AtTheEnd + "Index from ?$ rule must be a number.'" + str_aux + " Dealsite: " + dd.data[49] + "\n";
                            break;
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Index from the ?$ rule" + str_aux + "' is outside the range of an Int. Dealsite: " + dd.data[49] + "\n");
                            AtTheEnd = AtTheEnd + "Index from the ?$ rule" + str_aux + "' is outside the range of an Int. Dealsite: " + dd.data[49] + "\n";
                            break;
                        }
                        i = end + 1;
                        end = str_aux.IndexOf(';', i);
                        if (dd.data[num] == "")
                        {
                            dd.data[num] = str_aux.Substring(i, end - i);
                        }
                        str_aux = str_aux.Remove(0, end + 1);
                        i = str_aux.IndexOf("&$");
                    }

                }

                // Data Handling

                // if there is no DealLinkURL, the deal is invalid. So, go to the next deal
                if (dd.data[5] == "")
                    continue;

                for (i = 1; i < 50; i++)
                {
                    int b = dd.data[i].IndexOf("||");
                    if (b != -1)
                        dd.data[i] = dd.data[i].Remove(b);
                    if (i == 13)
                        RemoveSpaces(ref dd.data[i], true, false);
                    else
                        RemoveSpaces(ref dd.data[i], true, true);
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

                DealersPreProcessingData(dd, removeText);

                // If it is an online deal (i.e., there is no address, postal code, remove province, country and city. The advertised cities can be caught from OtherData.ListOfCities table. FullAddress will contain format number, that's why it is considered empty if its lenght is smaller than 5
                if ((dd.data[13].Length <= 5) && (dd.data[14] == "") && (dd.data[16] == ""))
                {
                    dd.data[15] = "";
                    dd.data[17] = "";
                    dd.data[43] = "";
                }

//                if (dd.data[13] != "")
                    locations = evalutatingFullAddress(dd, ref timesCalled, ref googleMapsCalled);

                if (dd.data[6] != "")
                    CheckCategory(ref dd.data[6]);
                PriceHandling(dd);
                isDealValid(ref dd.data[32], ref dd.data[33], ref dd.data[34]);
                GetExpiryTime(dd, ref AtTheEnd);
                VouchersHandling(dd);
// end of Data Handling

                SqlCommand myCommandDeal = null;
                SqlCommand myCommandOtherData = null;
                Boolean inList = false;
                string tempExpiryTime = "";

                try
                {
                    string DealValid = "";
                    string query = "";

                    if (dd.data[34] == "false")
                        query = "SELECT DealsEnded.Website, DealsEnded.DealID, DealValid, DealsEnded.ExpiryTime FROM DealsEnded, OtherData WHERE DealsEnded.Website = OtherData.Website AND DealsEnded.DealID = OtherData.DealID AND DealsEnded.Website = @Website AND DealsEnded.DealID = @DealID";
                    else
                        query = "SELECT DealsList.Website, DealsList.DealID, DealValid, DealsList.ExpiryTime FROM DealsList, OtherData WHERE DealsList.Website = OtherData.Website AND DealsList.DealID = OtherData.DealID AND DealsList.Website = @Website AND DealsList.DealID = @DealID";

                    using (SqlCommand myCommandChecker1 = new SqlCommand(query, myConnection))
                    {
                        SqlParameter checkWebsite = new SqlParameter();
                        checkWebsite.ParameterName = "@Website";
                        if (dd.data[0] == "")
                            checkWebsite.Value = DBNull.Value;
                        else
                            checkWebsite.Value = dd.data[0];
                        myCommandChecker1.Parameters.Add(checkWebsite);
                        
                        SqlParameter checkDealID = new SqlParameter();
                        checkDealID.ParameterName = "@DealID";
                        if (dd.data[4] == "")
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
                                tempExpiryTime = myChecker1["ExpiryTime"].ToString();
                            }
                        }
                    }    

                    if (!inList)
                    {
                        if (dd.data[34] == "false")
                            query = "SELECT DealsList.Website, DealsList.DealID, DealValid, DealsList.ExpiryTime FROM DealsList, OtherData WHERE DealsList.Website = OtherData.Website AND DealsList.DealID = OtherData.DealID AND DealsList.Website = @Website AND DealsList.DealID = @DealID";
                        else
                            query = "SELECT DealsEnded.Website, DealsEnded.DealID, DealValid, DealsEnded.ExpiryTime FROM DealsEnded, OtherData WHERE DealsEnded.Website = OtherData.Website AND DealsEnded.DealID = OtherData.DealID AND DealsEnded.Website = @Website AND DealsEnded.DealID = @DealID";

                        using (SqlCommand myCommandChecker2 = new SqlCommand(query, myConnection))
                        {
                            SqlParameter checkWebsite = new SqlParameter();
                            checkWebsite.ParameterName = "@Website";
                            if (dd.data[0] == "")
                                checkWebsite.Value = DBNull.Value;
                            else
                                checkWebsite.Value = dd.data[0];
                            myCommandChecker2.Parameters.Add(checkWebsite);
                        
                            SqlParameter checkDealID = new SqlParameter();
                            checkDealID.ParameterName = "@DealID";
                            if (dd.data[4] == "")
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
                                    tempExpiryTime = myChecker2["ExpiryTime"].ToString();
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
                                if (dd.data[29] != "")
                                    myCommandDeal = new SqlCommand("UPDATE DealsEnded SET DealLinkURL = @DealLinkURL, Category = @Category, Image = @Image, Description = @Description, DealerID = @DealerID, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, PayOutAmount = @PayOutAmount, PayOutLink = @PayOutLink, ExpiryTime = @ExpiryTime, MaxNumberVouchers = @MaxNumberOfVouchers, MinNumberVouchers = @MinNumberOfVouchers, PaidVoucherCount = @PaidVoucherCount, DealExtractedTime = @DealExtractedTime, Highlights = @Highlights, BuyDetails = @BuyDetails, DealText = @DealText, Reviews = @Reviews, DealSite = @DealSite, Currency = @Currency WHERE Website = @Website AND DealID = @DealID", myConnection);
                                else
                                    myCommandDeal = new SqlCommand("UPDATE DealsEnded SET DealLinkURL = @DealLinkURL, Category = @Category, Image = @Image, Description = @Description, DealerID = @DealerID, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, PayOutAmount = @PayOutAmount, PayOutLink = @PayOutLink, MaxNumberVouchers = @MaxNumberOfVouchers, MinNumberVouchers = @MinNumberOfVouchers, PaidVoucherCount = @PaidVoucherCount, DealExtractedTime = @DealExtractedTime, Highlights = @Highlights, BuyDetails = @BuyDetails, DealText = @DealText, Reviews = @Reviews, DealSite = @DealSite, Currency = @Currency WHERE Website = @Website AND DealID = @DealID", myConnection);
                                myCommandOtherData = new SqlCommand("UPDATE OtherData SET ListOfCities = @ListOfCities, SideDeals = @SideDeals, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, SecondsTotal = @SecondsTotal, SecondsElapsed = @SecondsElapsed, RemainingTime = @RemainingTime, ExpiryTime = @ExpiryTime, DealSoldOut = @DealSoldOut, DealEnded = @DealEnded, DealValid = @DealValid, RelatedDeals = @RelatedDeals WHERE Website = @Website AND DealID = @DealID", myConnection);
                                tempExpiryTime = "";
                            }
                            else if (dd.data[34] == "true")
                            {
                                myCommandDeal = new SqlCommand("UPDATE DealsList SET DealLinkURL = @DealLinkURL, Category = @Category, Image = @Image, Description = @Description, DealerID = @DealerID, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, PayOutAmount = @PayOutAmount, PayOutLink = @PayOutLink, ExpiryTime = @ExpiryTime, MaxNumberVouchers = @MaxNumberOfVouchers, MinNumberVouchers = @MinNumberOfVouchers, PaidVoucherCount = @PaidVoucherCount, DealExtractedTime = @DealExtractedTime, Highlights = @Highlights, BuyDetails = @BuyDetails, DealText = @DealText, Reviews = @Reviews, DealSite = @DealSite, Currency = @Currency WHERE Website = @Website AND DealID = @DealID", myConnection);
                                myCommandOtherData = new SqlCommand("UPDATE OtherData SET ListOfCities = @ListOfCities, SideDeals = @SideDeals, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, SecondsTotal = @SecondsTotal, SecondsElapsed = @SecondsElapsed, RemainingTime = @RemainingTime, ExpiryTime = @ExpiryTime, DealSoldOut = @DealSoldOut, DealEnded = @DealEnded, DealValid = @DealValid, RelatedDeals = @RelatedDeals WHERE Website = @Website AND DealID = @DealID", myConnection);
                                tempExpiryTime = "";
                            }
                        }
                        else
                        {
                            SqlCommand DeleteDeal = null;
                            if (dd.data[34] == "false")
                            {
                                // dont simply change tempExpiryTime. If it has a value, that should be used in place of the extracted one, in case the extracted one is NULL
                                if (dd.data[29] != "")
                                    tempExpiryTime = "";
                                myCommandDeal = new SqlCommand("INSERT INTO DealsEnded (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, Saved, Discount, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews, DealSite, Currency) Values (@Website, @DealID, @DealLinkURL, @Category, @Image, @Description, @DealerID, @RegularPrice, @OurPrice, @Saved, @Discount, @PayOutAmount, @PayOutLink, @ExpiryTime, @MaxNumberOfVouchers, @MinNumberOfVouchers, @PaidVoucherCount, @DealExtractedTime, @Highlights, @BuyDetails, @DealText, @Reviews, @DealSite, @Currency)", myConnection);
                                myCommandOtherData = new SqlCommand("UPDATE OtherData SET ListOfCities = @ListOfCities, SideDeals = @SideDeals, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, SecondsTotal = @SecondsTotal, SecondsElapsed = @SecondsElapsed, RemainingTime = @RemainingTime, ExpiryTime = @ExpiryTime, DealSoldOut = @DealSoldOut, DealEnded = @DealEnded, DealValid = @DealValid, RelatedDeals = @RelatedDeals WHERE Website = @Website AND DealID = @DealID", myConnection);
                                DeleteDeal = new SqlCommand("DELETE FROM DealsList WHERE Website = @Website AND DealID = @DealID", myConnection);
                            }
                            else
                            {
                                myCommandDeal = new SqlCommand("INSERT INTO DealsList (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, Saved, Discount, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews, DealSite, Currency) Values (@Website, @DealID, @DealLinkURL, @Category, @Image, @Description, @DealerID, @RegularPrice, @OurPrice, @Saved, @Discount, @PayOutAmount, @PayOutLink, @ExpiryTime, @MaxNumberOfVouchers, @MinNumberOfVouchers, @PaidVoucherCount, @DealExtractedTime, @Highlights, @BuyDetails, @DealText, @Reviews, @DealSite, @Currency)", myConnection);
                                myCommandOtherData = new SqlCommand("UPDATE OtherData SET ListOfCities = @ListOfCities, SideDeals = @SideDeals, RegularPrice = @RegularPrice, OurPrice = @OurPrice, Saved = @Saved, Discount = @Discount, SecondsTotal = @SecondsTotal, SecondsElapsed = @SecondsElapsed, RemainingTime = @RemainingTime, ExpiryTime = @ExpiryTime, DealSoldOut = @DealSoldOut, DealEnded = @DealEnded, DealValid = @DealValid, RelatedDeals = @RelatedDeals WHERE Website = @Website AND DealID = @DealID", myConnection);
                                DeleteDeal = new SqlCommand("DELETE FROM DealsEnded WHERE Website = @Website AND DealID = @DealID", myConnection);
                                tempExpiryTime = "";
                            }

                            SqlParameter checkWebsite = new SqlParameter();
                            checkWebsite.ParameterName = "@Website";
                            if (dd.data[0] == "")
                                checkWebsite.Value = DBNull.Value;
                            else
                                checkWebsite.Value = dd.data[0];
                            DeleteDeal.Parameters.Add(checkWebsite);
                        
                            SqlParameter checkDealID = new SqlParameter();
                            checkDealID.ParameterName = "@DealID";
                            if (dd.data[4] == "")
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
//                            tempExpiryTime = ""; IF inList =- False tempExpiryTime is already ""
                            myCommandDeal = new SqlCommand("INSERT INTO DealsEnded (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, Saved, Discount, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews, DealSite, Currency) Values (@Website, @DealID, @DealLinkURL, @Category, @Image, @Description, @DealerID, @RegularPrice, @OurPrice, @Saved, @Discount, @PayOutAmount, @PayOutLink, @ExpiryTime, @MaxNumberOfVouchers, @MinNumberOfVouchers, @PaidVoucherCount, @DealExtractedTime, @Highlights, @BuyDetails, @DealText, @Reviews, @DealSite, @Currency)", myConnection);
                            myCommandOtherData = new SqlCommand("INSERT INTO OtherData (Website, DealID, ListOfCities, SideDeals, RegularPrice, OurPrice, Saved, Discount, SecondsTotal, SecondsElapsed, RemainingTime, ExpiryTime, DealSoldOut, DealEnded, DealValid, RelatedDeals) Values (@Website, @DealID, @ListOfCities, @SideDeals, @RegularPrice, @OurPrice, @Saved, @Discount, @SecondsTotal, @SecondsElapsed, @RemainingTime, @ExpiryTime, @DealSoldOut, @DealEnded, @DealValid, @RelatedDeals)", myConnection);
                        }
                        else
                        {
//                            tempExpiryTime = ""; IF inList =- False tempExpiryTime is already ""
                            myCommandDeal = new SqlCommand("INSERT INTO DealsList (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, Saved, Discount, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews, DealSite, Currency) Values (@Website, @DealID, @DealLinkURL, @Category, @Image, @Description, @DealerID, @RegularPrice, @OurPrice, @Saved, @Discount, @PayOutAmount, @PayOutLink, @ExpiryTime, @MaxNumberOfVouchers, @MinNumberOfVouchers, @PaidVoucherCount, @DealExtractedTime, @Highlights, @BuyDetails, @DealText, @Reviews, @DealSite, @Currency)", myConnection);
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
                if (p41.Value.ToString() == "")
                    p41.Value = DBNull.Value;
                myCommandDeal.Parameters.Add(p41);
                
                SqlParameter p1 = new SqlParameter();
                p1.ParameterName = "@Website";
                if (dd.data[0] == "")
                    p1.Value = DBNull.Value;
                else
                    p1.Value = dd.data[0];
                myCommandDeal.Parameters.Add(p1);
                
                SqlParameter p2 = new SqlParameter();
                p2.ParameterName = "@DealID";
                if (dd.data[4] == "")
                    p2.Value = DBNull.Value;
                else
                    p2.Value = dd.data[4];
                myCommandDeal.Parameters.Add(p2);

                SqlParameter p3 = new SqlParameter();
                p3.ParameterName = "@DealLinkURL";
                if (dd.data[5] == "")
                    p3.Value = DBNull.Value;
                else
                    p3.Value = dd.data[5];
                myCommandDeal.Parameters.Add(p3);

                SqlParameter p4 = new SqlParameter();
                p4.ParameterName = "@Category";
                if (dd.data[6] == "")
                    p4.Value = DBNull.Value;
                else
                   p4.Value = dd.data[6];
                myCommandDeal.Parameters.Add(p4);

                SqlParameter p7 = new SqlParameter();
                p7.ParameterName = "@Image";
                if (dd.data[9] == "")
                    p7.Value = DBNull.Value;
                else
                    p7.Value = dd.data[9];
                myCommandDeal.Parameters.Add(p7);

                SqlParameter p8 = new SqlParameter();
                p8.ParameterName = "@Description";
                if (dd.data[10] == "")
                    p8.Value = DBNull.Value;
                else
                    p8.Value = dd.data[10];
                myCommandDeal.Parameters.Add(p8);

                SqlParameter p18 = new SqlParameter();
                p18.ParameterName = "@RegularPrice";
                if (dd.data[20] == "")
                    p18.Value = DBNull.Value;
                else
                    p18.Value = decimal.Parse(dd.data[20]);
                myCommandDeal.Parameters.Add(p18);

                SqlParameter p19 = new SqlParameter();
                p19.ParameterName = "@OurPrice";
                if (dd.data[21] == "")
                    p19.Value = DBNull.Value;
                else
                    p19.Value = decimal.Parse(dd.data[21]);
                myCommandDeal.Parameters.Add(p19);

                SqlParameter p20 = new SqlParameter();
                p20.ParameterName = "@Saved";
                if (dd.data[22] == "")
                    p20.Value = DBNull.Value;
                else
                    p20.Value = decimal.Parse(dd.data[22]);
                myCommandDeal.Parameters.Add(p20);

                SqlParameter p21 = new SqlParameter();
                p21.ParameterName = "@Discount";
                if (dd.data[23] == "")
                    p21.Value = DBNull.Value;
                else
                    p21.Value = decimal.Parse(dd.data[23]);
                myCommandDeal.Parameters.Add(p21);

                SqlParameter p22 = new SqlParameter();
                p22.ParameterName = "@PayOutAmount";
                if (dd.data[24] == "")
                    p22.Value = DBNull.Value;
                else
                    p22.Value = decimal.Parse(dd.data[24]);
                myCommandDeal.Parameters.Add(p22);

                SqlParameter p23 = new SqlParameter();
                p23.ParameterName = "@PayOutLink";
                if (dd.data[25] == "")
                {
                    if (dd.data[5] == "")
                        p23.Value = DBNull.Value;
                    else
                        p23.Value = dd.data[5];
                }
                else
                    p23.Value = dd.data[25];
                myCommandDeal.Parameters.Add(p23);

                SqlParameter p27 = new SqlParameter();
                p27.ParameterName = "@ExpiryTime";
                if (tempExpiryTime != "")
                    p27.Value = DateTimeOffset.Parse(tempExpiryTime);
                else
                {
                    if (dd.data[29] == "")
                        p27.Value = DBNull.Value;
                    else
                        p27.Value = DateTimeOffset.Parse(dd.data[29]);
                }
                myCommandDeal.Parameters.Add(p27);

                SqlParameter p28 = new SqlParameter();
                p28.ParameterName = "@MaxNumberOfVouchers";
                if (dd.data[30] == "")
                    p28.Value = DBNull.Value;
                else
                    p28.Value = Convert.ToInt32(dd.data[30]);
                myCommandDeal.Parameters.Add(p28);

                SqlParameter p29 = new SqlParameter();
                p29.ParameterName = "@MinNumberOfVouchers";
                if (dd.data[31] == "")
                    p29.Value = DBNull.Value;
                else
                    p29.Value = Convert.ToInt32(dd.data[31]);
                myCommandDeal.Parameters.Add(p29);

                SqlParameter p31 = new SqlParameter();
                p31.ParameterName = "@PaidVoucherCount";
                if (dd.data[35] == "")
                    p31.Value = DBNull.Value;
                else
                    p31.Value = Convert.ToInt32(dd.data[35]);
                myCommandDeal.Parameters.Add(p31);

                SqlParameter p32 = new SqlParameter();
                p32.ParameterName = "@Highlights";
                if (dd.data[36] == "")
                    p32.Value = DBNull.Value;
                else
                    p32.Value = dd.data[36];
                myCommandDeal.Parameters.Add(p32);

                SqlParameter p33 = new SqlParameter();
                p33.ParameterName = "@BuyDetails";
                if (dd.data[37] == "")
                    p33.Value = DBNull.Value;
                else
                    p33.Value = dd.data[37];
                myCommandDeal.Parameters.Add(p33);

                SqlParameter p34 = new SqlParameter();
                p34.ParameterName = "@DealText";
                if (dd.data[38] == "")
                    p34.Value = DBNull.Value;
                else
                    p34.Value = dd.data[38];
                myCommandDeal.Parameters.Add(p34);

                SqlParameter p35 = new SqlParameter();
                p35.ParameterName = "@Reviews";
                if (dd.data[39] == "")
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
                if (dd.data[49] == "")
                    p43.Value = DBNull.Value;
                else
                    p43.Value = dd.data[49];
                myCommandDeal.Parameters.Add(p43);

                SqlParameter p44 = new SqlParameter();
                p44.ParameterName = "@Currency";
                if (dd.data[46] == "")
                    p44.Value = DBNull.Value;
                else
                    p44.Value = dd.data[46];
                myCommandDeal.Parameters.Add(p44);

                SqlParameter p1a = new SqlParameter();
                p1a.ParameterName = "@Website";
                if (dd.data[0] == "")
                    p1a.Value = DBNull.Value;
                else
                    p1a.Value = dd.data[0];
                myCommandOtherData.Parameters.Add(p1a);

                SqlParameter p2a = new SqlParameter();
                p2a.ParameterName = "@DealID";
                if (dd.data[4] == "")
                    p2a.Value = DBNull.Value;
                else
                    p2a.Value = dd.data[4];
                myCommandOtherData.Parameters.Add(p2a);

                SqlParameter p18a = new SqlParameter();
                p18a.ParameterName = "@RegularPrice";
                if (dd.data[20] == "")
                    p18a.Value = DBNull.Value;
                else
                    p18a.Value = decimal.Parse(dd.data[20]);
                myCommandOtherData.Parameters.Add(p18a);

                SqlParameter p19a = new SqlParameter();
                p19a.ParameterName = "@OurPrice";
                if (dd.data[21] == "")
                    p19a.Value = DBNull.Value;
                else
                    p19a.Value = decimal.Parse(dd.data[21]);
                myCommandOtherData.Parameters.Add(p19a);

                SqlParameter p20a = new SqlParameter();
                p20a.ParameterName = "@Saved";
                if (dd.data[22] == "")
                    p20a.Value = DBNull.Value;
                else
                    p20a.Value = decimal.Parse(dd.data[22]);
                myCommandOtherData.Parameters.Add(p20a);

                SqlParameter p21a = new SqlParameter();
                p21a.ParameterName = "@Discount";
                if (dd.data[23] == "")
                    p21a.Value = DBNull.Value;
                else
                    p21a.Value = decimal.Parse(dd.data[23]);
                myCommandOtherData.Parameters.Add(p21a);

                SqlParameter p24 = new SqlParameter();
                p24.ParameterName = "@SecondsTotal";
                if (dd.data[26] == "")
                    p24.Value = DBNull.Value;
                else
                    p24.Value = dd.data[26];
                myCommandOtherData.Parameters.Add(p24);

                SqlParameter p25 = new SqlParameter();
                p25.ParameterName = "@SecondsElapsed";
                if (dd.data[27] == "")
                    p25.Value = DBNull.Value;
                else
                    p25.Value = dd.data[27];
                myCommandOtherData.Parameters.Add(p25);

                SqlParameter p26 = new SqlParameter();
                p26.ParameterName = "@RemainingTime";
                if (dd.data[28] == "")
                    p26.Value = DBNull.Value;
                else
                    p26.Value = dd.data[28];
                myCommandOtherData.Parameters.Add(p26);

                SqlParameter p27a = new SqlParameter();
                p27a.ParameterName = "@ExpiryTime";
                if (tempExpiryTime != "")
                    p27a.Value = DateTimeOffset.Parse(tempExpiryTime);
                else
                {
                    if (dd.data[29] == "")
                        p27a.Value = DBNull.Value;
                    else
                        p27a.Value = DateTimeOffset.Parse(dd.data[29]);
                }
                myCommandOtherData.Parameters.Add(p27a);

                SqlParameter p30 = new SqlParameter();
                p30.ParameterName = "@DealValid";
                if (dd.data[34] == "")
                    p30.Value = DBNull.Value;
                else
                    p30.Value = dd.data[34];
                myCommandOtherData.Parameters.Add(p30);

                SqlParameter p36 = new SqlParameter();
                p36.ParameterName = "@ListOfCities";
                if (dd.data[2] == "")
                    p36.Value = DBNull.Value;
                else
                    p36.Value = dd.data[2];
                myCommandOtherData.Parameters.Add(p36);

                SqlParameter p37 = new SqlParameter();
                p37.ParameterName = "@SideDeals";
                if (dd.data[3] == "")
                    p37.Value = DBNull.Value;
                else
                    p37.Value = dd.data[3];
                myCommandOtherData.Parameters.Add(p37);

                SqlParameter p38 = new SqlParameter();
                p38.ParameterName = "@DealSoldOut";
                if (dd.data[32] == "")
                    p38.Value = DBNull.Value;
                else
                    p38.Value = dd.data[32];
                myCommandOtherData.Parameters.Add(p38);

                SqlParameter p39 = new SqlParameter();
                p39.ParameterName = "@DealEnded";
                if (dd.data[33] == "")
                    p39.Value = DBNull.Value;
                else
                    p39.Value = dd.data[33];
                myCommandOtherData.Parameters.Add(p39);

                SqlParameter p40 = new SqlParameter();
                p40.ParameterName = "@RelatedDeals";
                if (dd.data[40] == "")
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
            writer.WriteLine("\nMethod called " + timesCalled + "times \n");
            writer.WriteLine("GoogleMaps called " + googleMapsCalled + "times \n");
            writer.Close();
            Console.WriteLine("Method called " + timesCalled + "times \n");
            Console.WriteLine("GoogleMaps called " + googleMapsCalled + "times \n");
            Console.WriteLine(DateTime.Now);

        }

        private void CheckCategory(ref string p)
        {
            p = p.ToLower();
            if (p.Contains("mobile") || p.Contains("at home") || p.Contains(" phone"))
                p = "Mobile";
            else if (p.Contains("online") || p.Contains("national"))
                p = "Online";
            else if (p.Contains("family") || p.Contains("families"))
                p = "Family";
            else if (p.Contains("hotel") || p.Contains("getaway") || p.Contains("escape") || p.Contains("travel"))
                p = "Escape";
            else if (p.Contains("dream"))
                p = "Incredible";
            else p = "";
        }
         
        private dealer evalutatingFullAddress(Tags dd, ref int timesCalled, ref int timesGoogleMaps)
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
            int format = 0;
            if (dd.data[13] != "")
            {
                if (dd.data[13][0] != '#')
                {
                    Console.WriteLine("Missing Address format.\n");
                    AtTheEnd = AtTheEnd + "Missiing Address format.\n";
                    dd.data[13] = "";
                    return locations;
                }
                int i = dd.data[13].IndexOf('#', 1);
                if (i == -1)
                {
                    Console.WriteLine("Wrong Address format.\n");
                    AtTheEnd = AtTheEnd + "Wrong Address format.\n";
                    dd.data[13] = "";
                    return locations;
                }

                try
                {
                    format = Convert.ToInt16(dd.data[13].Substring(1, i - 1));
                }
                catch (FormatException)
                {
                    Console.WriteLine("FullAddress DESCRIPTION format is not a number.'" + dd.data[13].Substring(1, i - 1) + " Dealsite: " + dd.data[49] + "\n");
                    AtTheEnd = AtTheEnd + "FullAddress DESCRIPTION format is not a number.'" + dd.data[13].Substring(1, i - 1) + " Dealsite: " + dd.data[49] + "\n";
                    return null;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("One of those format numbers'" + dd.data[31] + "' is outside the range of a Double (ExpiryTime).: " + " Dealsite: " + dd.data[49] + "\n");
                    AtTheEnd = AtTheEnd + "One of those format numbers'" + dd.data[31] + " is outside the range of a Double (ExpiryTime).: " + " Dealsite: " + dd.data[49] + "\n";
                    return null;
                }
                
                dd.data[13] = dd.data[13].Remove(0, i + 1);

                dd.data[13] = dd.data[13].Replace("</ul>", " ");
                dd.data[13] = dd.data[13].Replace("</u>", " ");
                dd.data[13] = dd.data[13].Replace("<ul>", " ");
                dd.data[13] = dd.data[13].Replace("<li>", ";");
            }
            // FullAddresses that are a mess. They will not be handled at this moment. Hope in the future they will adopt a format so we could handle it.
//            if (format == 2)
//                dd.data[13] = "";
            string country = dd.data[17].ToLower();

            if ((country == "usa") || (country == "états-unis") || (country == "stany zjednoczone") || (country == "verenigde staten"))
                dd.data[17] = "United States";
            else if (country == "francja")
                dd.data[17] = "France";
            else if ((country == "holandia") || (country == "Pays-Bas"))
                dd.data[17] = "Netherlands";
            else if ((country == "irlande") || (country == "irlandia"))
                dd.data[17] = "Ireland";
            else if (country == "kanada")
                dd.data[17] = "Canada";
            else if ((country == "nouvelle-zélande") || (country == "nowa zelandia"))
                dd.data[17] = "New Zealand";
            else if ((country == "royaume-uni") || (country == "wielka brytania"))
                dd.data[17] = "United Kingdom";

            if (dd.data[13] == "")
            {
//                if ((dd.data[14] != "") || (dd.data[15] != "") || (dd.data[43] != "") || (dd.data[17] != "") || (dd.data[16] != "") || (dd.data[19] != "") || (dd.data[11] != "") || (dd.data[12] != "") || (dd.data[18] != ""))
//                {
                    location dealer_address = new location();
                    dealer_address.SetContact(dd.data[19]);
                    dealer_address.SetPostalCode(dd.data[16]);
                    dealer_address.SetStreetAddress(dd.data[14]);
                    dealer_address.SetCity(dd.data[15]);
                    dd.data[43] = getProvince(dd.data[43], ref dd.data[17]);
                    dealer_address.SetProvince(dd.data[43]);
                    dealer_address.SetCountry(dd.data[17]);
                    if ((dd.data[11] == "") && (dd.data[12] == ""))
                    {
                        GetLatLong(dealer_address, dd);
                    }
                    else
                    {
                        RoundLatLong(ref dd.data[11], ref dd.data[12], ref AtTheEnd, dd.data[5]);
                        dealer_address.SetLatitude(dd.data[11]);
                        dealer_address.SetLongitude(dd.data[12]);
                    }
                    dealer_address.SetMap(CreateMapLink(dd));

                    if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                    {
                        if (dd.data[6].ToLower() != "mobile")
                        {
                            dealer_address.SetCity("");
                            dealer_address.SetProvince("");
                            dealer_address.SetCountry("");
                        }
                        dealer_address.SetLatitude("");
                        dealer_address.SetLongitude("");
                        dealer_address.SetMap("");
                    }


                    locations.SetLocation(dealer_address);
                    dd.data[13] = dd.data[14]!=""?(dd.data[14] + ", "):"";
                    dd.data[13] += dd.data[15]!=""?(dd.data[15] + ", "):"";
                    dd.data[13] += dd.data[43]!=""?(dd.data[43] + ", "):"";
                    dd.data[13] += dd.data[17]!=""?(dd.data[17] + ", "):"";
                    dd.data[13] += dd.data[16]!=""?(dd.data[16] + ", "):"";
                    dd.data[13] += dd.data[19] != "" ? dd.data[19] : "";
                    RemoveSpaces(ref dd.data[13], true, true);
  //              }
                return locations;
            }

            if (format == 1) // 1.	City, [province], street, Postal Code, [Contact] | Contact (EX: DealTicker)
            {
                string fullAddress = dd.data[13];
                string aux;
                dd.data[13] = "";

                while (fullAddress.Length > 0)
                {
                    location dealer_address = new location();
                    int i = fullAddress.LastIndexOf(',');
                    if (i == -1)
                        i = 0;
                    aux = fullAddress.Substring(i, fullAddress.Length - i);
                    RemoveSpaces(ref aux, true, true);
                    if (aux.Length > 7) // just to be sure it is not the postal code
                    {
                        fullAddress = fullAddress.Remove(i);
//                        RemoveSpaces(ref aux, true, true);
                        dealer_address.SetContact(aux);
                    }
                    if (fullAddress.Length > 0)
                    {
                        i = fullAddress.LastIndexOf(',');
                        if (i == -1)
                            i = 0;
                        aux = fullAddress.Substring(i, fullAddress.Length - i);
                        fullAddress = fullAddress.Remove(i);
                        RemoveSpaces(ref aux, true, true);
                        if (fullAddress == "")
                            dealer_address.SetCity(aux);
                        else
                        {
                            dealer_address.SetPostalCode(aux);

                            int b = fullAddress.LastIndexOf(';');
                            b += 1;
                            i = fullAddress.IndexOf(',', b);
                            if (i == -1)
                                i = fullAddress.Length - 1;
                            aux = fullAddress.Substring(b, i - b + 1);
                            fullAddress = fullAddress.Remove(b, i - b + 1);
                            RemoveSpaces(ref aux, true, true);
                            dealer_address.SetCity(aux);

                            i = fullAddress.LastIndexOf(';');
                            if (i == -1)
                                i = 0;
                            aux = fullAddress.Substring(i, fullAddress.Length - i);
                            fullAddress = fullAddress.Remove(i);
                            RemoveSpaces(ref aux, true, true);

                            i = aux.IndexOf(',');
                            if ((i != -1) && (i == 2))
                            {
                                string aux2 = aux.Substring(0, i);
                                RemoveSpaces(ref aux2, true, true);
                                aux = aux.Substring(i + 1, aux.Length - (i + 1));
                                RemoveSpaces(ref aux, true, true);
                                dealer_address.SetProvince(getProvince(aux2, ref dd.data[17]));
                            }
                            dealer_address.SetStreetAddress(aux);
                        }
                    }

                    if ((fullAddress.Length == 0) && (locations.CountLocations() == 0))
                    {
                        RemoveSpaces(ref dd.data[19], true, true);
                        if (dd.data[19] != "")
                        {
                            string tempContact = dealer_address.GetContact();
                            if (!tempContact.Contains(dd.data[19]))
                                dealer_address.SetContact(tempContact + "; " + dd.data[19]);
                        }
                        dd.data[43] = getProvince(dd.data[43], ref dd.data[17]);
                        dealer_address.SetProvince(dd.data[43]);
                        if ((dd.data[11] == "") && (dd.data[12] == ""))
                        {
                            GetLatLong(dealer_address, dd);
                        }
                        else
                        {
                            RoundLatLong(ref dd.data[11], ref dd.data[12], ref AtTheEnd, dd.data[5]);
                            dealer_address.SetLatitude(dd.data[11]);
                            dealer_address.SetLongitude(dd.data[12]);
                        }
                        dealer_address.SetMap(CreateMapLink(dd));
                    }
                    else
                    {
                        // Still don't know if DealTicker can have more than one location, so, I don't know if it will includes all of the lat/longit
                        //  ??? What about Province and Country? ?????????????????????????????????????????????????????
                        AtTheEnd = AtTheEnd + "WARNING: There is no Province/Country...\n";
                        GetLatLong(dealer_address, dd);
                        dealer_address.SetMap(CreateMapLink(dd));
                    }

                    dealer_address.SetCountry(dd.data[17]);

                    if (dd.data[19] != "")
                    { // contacts can have more than one, like phones and emails
                        string aux1 = dealer_address.GetContact();
                        do
                        {
                            i = dd.data[19].LastIndexOf(";");
                            if (i == -1)
                                i = 0;
                            aux = dd.data[19].Substring(i, dd.data[19].Length - i);
                            dd.data[19] = dd.data[19].Remove(i);
                            RemoveSpaces(ref aux, true, true);
                            if (aux1 == "")
                                aux1 = aux;
                            else if (!aux1.Contains(aux))
                            {
                                aux1 = aux1 + "; " + aux;
                            }
                        } while (dd.data[19] != "");
                        dealer_address.SetContact(aux1);
                    }

                    if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                    {
                        if (dd.data[6].ToLower() != "mobile")
                        {
                            dealer_address.SetCity("");
                            dealer_address.SetProvince("");
                            dealer_address.SetCountry("");
                        }
                        dealer_address.SetLatitude("");
                        dealer_address.SetLongitude("");
                        dealer_address.SetMap("");
                    }
                    else
                    {
                        if (dealer_address.GetStreetAddress() == "")
                            dealer_address.SetStreetAddress(dd.data[14]);
                        if (dealer_address.GetCity() == "")
                            dealer_address.SetCity(dd.data[15]);
                        if (dealer_address.GetPostalCode() == "")
                            dealer_address.SetPostalCode(dd.data[16]);
                        if (dealer_address.GetCountry() == "")
                            dealer_address.SetCountry(dd.data[17]);
                        if (dealer_address.GetProvince() == "")
                            dealer_address.SetProvince(dd.data[43]);
                    }

                    locations.SetLocation(dealer_address);
                }
            }
            if (format == 2) 
//          This FullAddresses is a mess. It will not be handled at this moment. Hope in the future it will adopt a format so we could handle it. 
            {
                string fullAddress = dd.data[13];
                fullAddress = fullAddress.Replace(",;", ",");
                if (fullAddress.Contains(";;;"))
                {
                    fullAddress = fullAddress.Replace(";;", ";");
                    while (fullAddress.Contains(";;;"))
                        fullAddress = fullAddress.Replace(";;;", ";;");
                }
                dd.data[13] = "";
                string aux1, aux2;
                int i;
                string contact = dd.data[19];
                Boolean gotAtLeastOne = false;

                while (fullAddress != "")
                {
                    RemoveSpaces(ref fullAddress, true, true);
                    location dealer_address = new location();
                    Boolean got = false;
                    i = fullAddress.IndexOf(";;");
                    if (i == -1)
                        i = fullAddress.Length;
                    aux1 = fullAddress.Substring(0, i);
                    RemoveSpaces(ref aux1, true, true);
                    fullAddress = fullAddress.Remove(0, i);

                    got = GetAddressFromGoogleMaps(ref aux1, ref dealer_address, dd.data[5], ref timesCalled, ref timesGoogleMaps, dd.data[43]);

                    if (got)
                    {
                        gotAtLeastOne = true;

                        aux1 = aux1.Replace(':', ';');
                        while (aux1 != "") // look for phone information in  aux1
                        {
                            Boolean isPhone = true;
                            RemoveSpaces(ref aux1, true, true);

                            i = aux1.LastIndexOf(';');
                            if (i == -1)
                                i = 0;
                            string aux3 = aux1.Substring(i, aux1.Length - i);
                            aux1 = aux1.Remove(i);
                            RemoveSpaces(ref aux3, true, true);

                            aux2 = aux3.Replace(" ", "");
                            aux2 = aux2.Replace("(", "");
                            aux2 = aux2.Replace(")", "");
                            aux2 = aux2.Replace("-", "");
                            aux2 = aux2.Replace(".", "");

                            if (((aux2.Length >= 6) && (aux2.Length <= 12)) || ((aux2.Length >= 20) && (aux2.Length <= 24)))
                            {
                                try
                                {
                                    // if first 6 digits are number, it is considered a phone number
                                    int test = Convert.ToInt32(aux2.Substring(0, 6));
                                }
                                catch (FormatException)
                                {
                                    try
                                    {
                                        int test = Convert.ToInt32(aux2.Substring(0, 3));
 //                                                test = Convert.ToInt32(aux2.Substring(aux2.Length - 3, 3));
                                        if ((aux3[3] != '-') || (aux3[7] != '-'))
                                            isPhone = false;
                                    }
                                    catch (FormatException)
                                    {
                                        isPhone = false;
                                    }
                                }
                            }
                            else
                                isPhone = false;

                            if (isPhone)
                            {
                                dealer_address.SetContact(aux3);
                                aux1 = "";
                            }
                        }

                        if (dd.data[19] != "")
                        { // contacts can have more than one, like phones and emails
                            aux1 = dealer_address.GetContact();
                            do
                            {
                                i = dd.data[19].LastIndexOf(";");
                                if (i == -1)
                                    i = 0;
                                string aux = dd.data[19].Substring(i, dd.data[19].Length - i);
                                dd.data[19] = dd.data[19].Remove(i);
                                RemoveSpaces(ref aux, true, true);
                                if (aux1 == "")
                                    aux1 = aux;
                                else if (!aux1.Contains(aux))
                                {
                                    aux1 = aux1 + "; " + aux;
                                }
                            } while (dd.data[19] != "");
                            dealer_address.SetContact(aux1);
                        }

                        if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                        {
                            if (dd.data[6].ToLower() != "mobile")
                            {
                                dealer_address.SetCity("");
                                dealer_address.SetProvince("");
                                dealer_address.SetCountry("");
                            }
                            dealer_address.SetLatitude("");
                            dealer_address.SetLongitude("");
                            dealer_address.SetMap("");
                        }
                        else
                        {
                            if (dealer_address.GetStreetAddress() == "")
                                dealer_address.SetStreetAddress(dd.data[14]);
                            if (dealer_address.GetCity() == "")
                                dealer_address.SetCity(dd.data[15]);
                            if (dealer_address.GetPostalCode() == "")
                                dealer_address.SetPostalCode(dd.data[16]);
                            if (dealer_address.GetCountry() == "")
                                dealer_address.SetCountry(dd.data[17]);
                            if (dealer_address.GetProvince() == "")
                                dealer_address.SetProvince(dd.data[43]);
                        }
                        locations.SetLocation(dealer_address);
                    }
                }
                if (!gotAtLeastOne)
                {
                    dd.data[13] = "#2#";
                    // in case there are data in 14, 15, 16, 17, 19 and/or 43, get them
                    return (evalutatingFullAddress(dd, ref timesCalled, ref timesGoogleMaps));
                }
            }
            if (format == 3) // WagJag. Has 3 different formats... 
            {
                string fullAddress = dd.data[13];
                fullAddress = fullAddress.Replace(", ", ",");
                RemoveSpaces(ref fullAddress, true, true);
                string aux1, aux2;
                dd.data[13] = "";
                int i;

                if (fullAddress != "")
                {
                    if ((fullAddress.Length > 10) && (fullAddress.Substring(0, 10) == "sites = [["))
                    {
                        i = fullAddress.IndexOf("/$/");
                        if (i != -1)
                        {
                            if (i < 30)
                                fullAddress = fullAddress.Remove(0, i + 3);
                            else
                                fullAddress = fullAddress.Remove(i);
                        }
                    }
                    if ((fullAddress.Length > 10) && (fullAddress.Substring(0, 10) == "sites = [["))
                    {
                        if (fullAddress.Length < 30) 
                            AtTheEnd = AtTheEnd + "FullAddress with no valid data: " + fullAddress + " Dealsite: " + dd.data[5];
                        else
                        {
                            while (fullAddress.Length > 0)
                            {
                                location dealer_address = new location();

                                i = fullAddress.IndexOf('[');
                                fullAddress = fullAddress.Remove(0, i + 1);

                                i = fullAddress.IndexOf('"', 2);
                                int i1 = fullAddress.IndexOf("'", 2);
                                if ((i == -1) || ((i1 != -1) && (i1 < i)))
                                    i = i1;
                                i = fullAddress.IndexOf(',', i);
                                fullAddress = fullAddress.Remove(0, i + 1);

                                // get latitude
                                i = fullAddress.IndexOf(',');
                                aux1 = fullAddress.Substring(0, i);
                                fullAddress = fullAddress.Remove(0, i + 1);
                                RemoveSpaces(ref aux1, true, true);

                                // get longitude
                                i = fullAddress.IndexOf(',');
                                aux2 = fullAddress.Substring(0, i);
                                fullAddress = fullAddress.Remove(0, i + 1);
                                RemoveSpaces(ref aux2, true, true);

                                dd.data[11] = aux1;
                                dd.data[12] = aux2;

                                i = fullAddress.IndexOf('"');
                                if (i == -1)
                                    i = fullAddress.Length;
                                i1 = fullAddress.IndexOf('\'');
                                if ((i1 != -1) && (i1 < i))
                                    i = i1;
                                fullAddress = fullAddress.Remove(0, i + 1);

                                i = fullAddress.IndexOf('"');
                                if (i == -1)
                                    i = fullAddress.Length;
                                i1 = fullAddress.IndexOf('\'');
                                if ((i1 != -1) && (i1 < i))
                                    i = i1;

                                string partial = fullAddress.Substring(0, i);
                                if (i != fullAddress.Length)
                                    fullAddress = fullAddress.Remove(0, i + 1);
                                else
                                    fullAddress = "";

                                i = partial.LastIndexOf(',');
                                if (i == -1)
                                    i = 0;
                                aux2 = partial.Substring(i, partial.Length - i);
                                RemoveSpaces(ref aux2, true, true);
                                partial = partial != ""?partial.Remove(i):"";

                                if (partial != "")
                                {
                                    i = partial.LastIndexOf(',');
                                    if (i == -1)
                                        i = 0;
                                    i = partial.IndexOf(' ', i);
                                    if (i != -1)
                                    {
                                        aux1 = partial.Substring(i, partial.Length - i);
                                        partial = partial.Remove(i);
                                        RemoveSpaces(ref aux1, true, true);
                                        dealer_address.SetPostalCode(aux1);
                                    }

                                    i = partial.LastIndexOf(',');
                                    if (i == -1)
                                        i = 0;
                                    aux1 = partial.Substring(i, partial.Length - i);
                                    RemoveSpaces(ref aux1, true, true);
                                    partial = partial.Remove(i);
                                    dealer_address.SetProvince(getProvince(aux1, ref aux2));

                                    if (partial != "")
                                    {
                                        i = partial.LastIndexOf(',');
                                        if (i == -1)
                                            i = 0;
                                        aux1 = partial.Substring(i, partial.Length - i);
                                        RemoveSpaces(ref aux1, true, true);
                                        partial = partial.Remove(i);
                                        dealer_address.SetCity(aux1);
                                    }

                                    RemoveSpaces(ref partial, true, true);
                                    dealer_address.SetStreetAddress(partial);
                                }

                                dealer_address.SetCountry(aux2);

                                if ((dd.data[11] == "") && (dd.data[12] == ""))
                                {
                                    GetLatLong(dealer_address, dd);
                                }
                                else
                                {
                                    RoundLatLong(ref dd.data[11], ref dd.data[12], ref AtTheEnd, dd.data[5]);
                                    dealer_address.SetLatitude(dd.data[11]);
                                    dealer_address.SetLongitude(dd.data[12]);
                                }
                                dealer_address.SetMap(CreateMapLink(dd));

                                if (dd.data[19] != "")
                                { // contacts can have more than one, like phones and emails
                                    aux1 = dealer_address.GetContact();
                                    do
                                    {
                                        i = dd.data[19].LastIndexOf(";");
                                        if (i == -1)
                                            i = 0;
                                        string aux = dd.data[19].Substring(i, dd.data[19].Length - i);
                                        dd.data[19] = dd.data[19].Remove(i);
                                        RemoveSpaces(ref aux, true, true);
                                        if (aux1 == "")
                                            aux1 = aux;
                                        else if (!aux1.Contains(aux))
                                        {
                                            aux1 = aux1 + "; " + aux;
                                        }
                                    } while (dd.data[19] != "");
                                    dealer_address.SetContact(aux1);
                                }

                                if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                                {
                                    if (dd.data[6].ToLower() != "mobile")
                                    {
                                        dealer_address.SetCity("");
                                        dealer_address.SetProvince("");
                                        dealer_address.SetCountry("");
                                    }
                                    dealer_address.SetLatitude("");
                                    dealer_address.SetLongitude("");
                                    dealer_address.SetMap("");
                                }
                                else
                                {
                                    if (dealer_address.GetStreetAddress() == "")
                                        dealer_address.SetStreetAddress(dd.data[14]);
                                    if (dealer_address.GetCity() == "")
                                        dealer_address.SetCity(dd.data[15]);
                                    if (dealer_address.GetPostalCode() == "")
                                        dealer_address.SetPostalCode(dd.data[16]);
                                    if (dealer_address.GetCountry() == "")
                                        dealer_address.SetCountry(dd.data[17]);
                                    if (dealer_address.GetProvince() == "")
                                        dealer_address.SetProvince(dd.data[43]);
                                }

                                locations.SetLocation(dealer_address);
                            }
                        }
                    }
                    else
                    {
                        // removing comments between ()
                        i = fullAddress.IndexOf('(');
                        while (i != -1)
                        {
                            int j = fullAddress.IndexOf(')', i + 1);
                            if (j != -1)
                                fullAddress = fullAddress.Replace(fullAddress.Substring(i, j - i + 1), "");
                            else
                                fullAddress = fullAddress.Replace(fullAddress.Substring(i, fullAddress.Length - i), "");
                            i = fullAddress.IndexOf('(');
                        }

                        string contact = dd.data[19];
                        Boolean gotAtLeastOne = false;

                        while (fullAddress != "")
                        {
                            RemoveSpaces(ref fullAddress, true, true);
                            location dealer_address = new location();
                            Boolean got = false;
                            i = fullAddress.IndexOf(";;");
                            if (i == -1)
                                i = fullAddress.Length;
                            aux1 = fullAddress.Substring(0, i);
                            RemoveSpaces(ref aux1, true, true);
                            fullAddress = fullAddress.Remove(0, i);

                            got = GetAddressFromGoogleMaps(ref aux1, ref dealer_address, dd.data[5], ref timesCalled, ref timesGoogleMaps, dd.data[43]);

                            if (got)
                            {
                                gotAtLeastOne = true;

                                aux1 = aux1.Replace(':', ';');
                                while (aux1 != "") // look for phone information in aux1
                                {
                                    Boolean isPhone = true;
                                    RemoveSpaces(ref aux1, true, true);

                                    i = aux1.LastIndexOf(';');
                                    if (i == -1)
                                        i = 0;
                                    string aux3 = aux1.Substring(i, aux1.Length - i);
                                    aux1 = aux1.Remove(i);
                                    RemoveSpaces(ref aux3, true, true);

                                    aux2 = aux3.Replace(" ", "");
                                    aux2 = aux2.Replace("(", "");
                                    aux2 = aux2.Replace(")", "");
                                    aux2 = aux2.Replace("-", "");
                                    aux2 = aux2.Replace(".", "");

                                    if (((aux2.Length >= 6) && (aux2.Length <= 12)) || ((aux2.Length >= 20) && (aux2.Length <= 24)))
                                    {
                                        try
                                        {
                                            // if first 6 digits are number, it is considered a phone number
                                            int test = Convert.ToInt32(aux2.Substring(0, 6));
                                        }
                                        catch (FormatException)
                                        {
                                            try
                                            {
                                                int test = Convert.ToInt32(aux2.Substring(0, 3));
//                                                test = Convert.ToInt32(aux2.Substring(aux2.Length - 3, 3));
                                                if ((aux3[3] != '-') || (aux3[7] != '-'))
                                                    isPhone = false;
                                            }
                                            catch (FormatException)
                                            {
                                                isPhone = false;
                                            }
                                        }
                                    }
                                    else
                                        isPhone = false;

                                    if (isPhone)
                                    {
                                        dealer_address.SetContact(aux3);
                                        aux1 = "";
                                    }
                                }

                                if (dd.data[19] != "")
                                { // contacts can have more than one, like phones and emails
                                    aux1 = dealer_address.GetContact();
                                    do
                                    {
                                        i = dd.data[19].LastIndexOf(";");
                                        if (i == -1)
                                            i = 0;
                                        string aux = dd.data[19].Substring(i, dd.data[19].Length - i);
                                        dd.data[19] = dd.data[19].Remove(i);
                                        RemoveSpaces(ref aux, true, true);
                                        if (aux1 == "")
                                            aux1 = aux;
                                        else if (!aux1.Contains(aux))
                                        {
                                            aux1 = aux1 + "; " + aux;
                                        }
                                    } while (dd.data[19] != "");
                                    dealer_address.SetContact(aux1);
                                }

                                if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                                {
                                    if (dd.data[6].ToLower() != "mobile")
                                    {
                                        dealer_address.SetCity("");
                                        dealer_address.SetProvince("");
                                        dealer_address.SetCountry("");
                                    }
                                    dealer_address.SetLatitude("");
                                    dealer_address.SetLongitude("");
                                    dealer_address.SetMap("");
                                }
                                else
                                {
                                    if (dealer_address.GetStreetAddress() == "")
                                        dealer_address.SetStreetAddress(dd.data[14]);
                                    if (dealer_address.GetCity() == "")
                                        dealer_address.SetCity(dd.data[15]);
                                    if (dealer_address.GetPostalCode() == "")
                                        dealer_address.SetPostalCode(dd.data[16]);
                                    if (dealer_address.GetCountry() == "")
                                        dealer_address.SetCountry(dd.data[17]);
                                    if (dealer_address.GetProvince() == "")
                                        dealer_address.SetProvince(dd.data[43]);
                                }
                                locations.SetLocation(dealer_address);
                            }
                        }
                        if (!gotAtLeastOne)
                        {
                            dd.data[13] = "#2#";
                            // in case there are data in 14, 15, 16, 17, 19 and/or 43, get them
                            return (evalutatingFullAddress(dd, ref timesCalled, ref timesGoogleMaps));
                        }
                    }
                    /*                    {
                                            // removing comments between ()
                                            i = fullAddress.IndexOf('(');
                                            while (i != -1)
                                            {
                                                int j = fullAddress.IndexOf(')', i + 1);
                                                if (j != -1)
                                                    fullAddress = fullAddress.Replace(fullAddress.Substring(i, j - i + 1), "");
                                                else
                                                    fullAddress = fullAddress.Replace(fullAddress.Substring(i, fullAddress.Length - i), "");
                                                i = fullAddress.IndexOf('(');
                                            }
                                            RemoveSpaces(ref fullAddress, true, true);
                                            if ((fullAddress[0] >= '0') && (fullAddress[0] <= '9'))
                                            {
                                                while (fullAddress.Length > 0)
                                                {
                                                    location dealer_address = new location();

                                                    i = fullAddress.LastIndexOf(';');
                                                    aux1 = fullAddress.Substring(i, fullAddress.Length - i);
                                                    fullAddress = fullAddress.Remove(i);
                                                    RemoveSpaces(ref aux1, true, true);
                                                    dealer_address.SetPostalCode(aux1);

                                                    i = fullAddress.LastIndexOf(' ');
                                                    if (i == -1)
                                                        i = 0;
                                                    int i1 = fullAddress.LastIndexOf(',');
                                                    if (i1 == -1)
                                                        i1 = 0;
                                                    if (i1 > i)
                                                        i = i1;
                                                    aux1 = fullAddress.Substring(i, fullAddress.Length - i);
                                                    fullAddress = fullAddress.Remove(i);
                                                    RemoveSpaces(ref aux1, true, true);
                                                    dealer_address.SetProvince(getProvince(aux1, dd.data[17]));
                                                    dealer_address.SetCountry(dd.data[17]);

                                                    i = fullAddress.LastIndexOf(';');
                                                    aux1 = fullAddress.Substring(i, fullAddress.Length - i);
                                                    fullAddress = fullAddress.Remove(i);
                                                    RemoveSpaces(ref aux1, true, true);
                                                    dealer_address.SetCity(aux1);

                                                    i = fullAddress.LastIndexOf(";;");
                                                    if (i == -1)
                                                        i = 0;
                                                    aux1 = fullAddress.Substring(i, fullAddress.Length - i);
                                                    fullAddress = fullAddress.Remove(i);
                                                    RemoveSpaces(ref aux1, true, true);
                                                    dealer_address.SetStreetAddress(aux1);

                                                    GetLatLong(dealer_address, dd);
                                                    dealer_address.SetMap(CreateMapLink(dd));

                                                    if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                                                    {
                                                        dealer_address.SetCity("");
                                                        dealer_address.SetProvince("");
                                                        dealer_address.SetCountry("");
                                                        dealer_address.SetLatitude("");
                                                        dealer_address.SetLongitude("");
                                                        dealer_address.SetMap("");
                                                    }
                                                    else
                                                    {
                                                        if (dealer_address.GetStreetAddress() == "")
                                                            dealer_address.SetStreetAddress(dd.data[14]);
                                                        if (dealer_address.GetCity() == "")
                                                            dealer_address.SetCity(dd.data[15]);
                                                        if (dealer_address.GetPostalCode() == "")
                                                            dealer_address.SetPostalCode(dd.data[16]);
                                                        if (dealer_address.GetCountry() == "")
                                                            dealer_address.SetCountry(dd.data[17]);
                                                        if (dd.data[19] != "")
                                                        { // contacts can have more than one, like phones and emails
                                                            aux1 = dealer_address.GetContact();
                                                            do
                                                            {
                                                                i = dd.data[19].LastIndexOf(";");
                                                                if (i == -1)
                                                                    i = 0;
                                                                string aux = dd.data[19].Substring(i, dd.data[19].Length - i);
                                                                dd.data[19] = dd.data[19].Remove(i);
                                                                RemoveSpaces(ref aux, true, true);
                                                                if (aux1 == "")
                                                                    aux1 = aux;
                                                                else if (!aux1.Contains(aux))
                                                                {
                                                                    aux1 = aux1 + "; " + aux;
                                                                }
                                                            } while (dd.data[19] != "");
                                                            dealer_address.SetContact(aux1);
                                                        }
                                                        if (dealer_address.GetProvince() == "")
                                                            dealer_address.SetProvince(dd.data[43]);
                                                    }

                                                    locations.SetLocation(dealer_address);
                                                }

                                            }
                                            else
                                            {
                                                while (fullAddress.Length > 0)
                                                {
                                                    location dealer_address = new location();

                                                    i = fullAddress.IndexOf("<li>");
                                                    if (i != -1)
                                                        fullAddress = fullAddress.Remove(0, i + 4);

                                                    i = fullAddress.IndexOf(':');
                                                    if (i == -1)
                                                        i = fullAddress.Length;
                                                    int i1 = fullAddress.IndexOf(',');
                                                    if (i1 == -1)
                                                        i1 = fullAddress.Length; ;

                                                    if (i1 < i) // comma comes first
                                                    {
                                                        aux1 = fullAddress.Substring(i1, i - i1);
                                                        RemoveSpaces(ref aux1, true, true);
                                                        dealer_address.SetProvince(getProvince(aux1, dd.data[17]));

                                                        int i2 = fullAddress.LastIndexOf(';', i1);
                                                        if (i2 == -1)
                                                            i2 = 0;
                                                        aux1 = fullAddress.Substring(i2, i1 - i2);
                                                        RemoveSpaces(ref aux1, true, true);
                                                        dealer_address.SetCity(aux1);
                                                    }
                                                    else
                                                    {

                                                        int i2 = fullAddress.LastIndexOf(';', i == fullAddress.Length ? i - 1 : i);
                                                        if (i2 == -1)
                                                            i2 = 0;
                                                        aux1 = fullAddress.Substring(i2, i - i2);
                                                        RemoveSpaces(ref aux1, true, true);
                                                        dealer_address.SetCity(aux1);
                                                    }
                                                    fullAddress = fullAddress.Remove(0, i == fullAddress.Length ? i : i + 1);

                                                    dealer_address.SetCountry(dd.data[17]);

                                                    if (fullAddress != "")
                                                    {
                                                        i = fullAddress.IndexOf(';');
                                                        if (i == -1)
                                                            i = fullAddress.Length;

                                                        i1 = fullAddress.LastIndexOf(',', i == fullAddress.Length ? i - 1 : i);
                                                        if (i1 != -1)
                                                        {
                                                            aux1 = fullAddress.Substring(i1, i - i1);
                                                            RemoveSpaces(ref aux1, true, true);
                                                            if (aux1.Length < 3)
                                                            {
                                                                // provincia
                                                                if (dealer_address.GetProvince() == "")
                                                                {
                                                                    dealer_address.SetProvince(getProvince(aux1, dd.data[17]));
                                                                }
                                                            }
                                                        }

                                                        aux1 = fullAddress.Substring(0, i);
                                                        fullAddress = fullAddress != "" ? fullAddress.Remove(0, i) : "";
                                                        RemoveSpaces(ref aux1, true, true);
                                                        dealer_address.SetStreetAddress(aux1);
                                                    }

                                                    GetLatLong(dealer_address, dd);
                                                    dealer_address.SetMap(CreateMapLink(dd));

                                                    if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                                                    {
                                                        dealer_address.SetCity("");
                                                        dealer_address.SetProvince("");
                                                        dealer_address.SetCountry("");
                                                        dealer_address.SetLatitude("");
                                                        dealer_address.SetLongitude("");
                                                        dealer_address.SetMap("");
                                                    }
                                                    else
                                                    {
                                                        if (dealer_address.GetStreetAddress() == "")
                                                            dealer_address.SetStreetAddress(dd.data[14]);
                                                        if (dealer_address.GetCity() == "")
                                                            dealer_address.SetCity(dd.data[15]);
                                                        if (dealer_address.GetPostalCode() == "")
                                                            dealer_address.SetPostalCode(dd.data[16]);
                                                        if (dealer_address.GetCountry() == "")
                                                            dealer_address.SetCountry(dd.data[17]);
                                                        if (dd.data[19] != "")
                                                        { // contacts can have more than one, like phones and emails
                                                            aux1 = dealer_address.GetContact();
                                                            do
                                                            {
                                                                i = dd.data[19].LastIndexOf(";");
                                                                if (i == -1)
                                                                    i = 0;
                                                                string aux = dd.data[19].Substring(i, dd.data[19].Length - i);
                                                                dd.data[19] = dd.data[19].Remove(i);
                                                                RemoveSpaces(ref aux, true, true);
                                                                if (aux1 == "")
                                                                    aux1 = aux;
                                                                else if (!aux1.Contains(aux))
                                                                {
                                                                    aux1 = aux1 + "; " + aux;
                                                                }
                                                            } while (dd.data[19] != "");
                                                            dealer_address.SetContact(aux1);
                                                        }
                                                        if (dealer_address.GetProvince() == "")
                                                            dealer_address.SetProvince(dd.data[43]);
                                                    }

                                                    locations.SetLocation(dealer_address);
                                                }
                                            }
                                        }*/
                }

            }
            if (format == 4) // google maps description and text, from TeamBuy
            {
                Boolean gotAtLeastOne = false;
                string fullAddress = dd.data[13];
                if ((fullAddress.Length > 5) & (fullAddress.Substring(0, 5) == "?f=q&"))
                    fullAddress = "&" + fullAddress; // just to avoid the ? to be removed when executing removespaces
                RemoveSpaces(ref fullAddress, true, true);
                string aux1, aux2;
                dd.data[13] = "";
                int i = fullAddress.IndexOf("?f=q&");

                if (i != -1)
                {
                    while (i != -1)
                    {
                        location dealer_address = new location();
                        i = fullAddress.LastIndexOf('%', i);

                        int i1 = fullAddress.IndexOf(';', i);
                        if (i1 == -1)
                            i1 = fullAddress.Length;

                        string partial = fullAddress.Substring(i, i1 - i);
                        fullAddress = fullAddress.Remove(i, i1 - i);
                        RemoveSpaces(ref fullAddress, true, true);

                        partial = partial.Replace('+', ' ');

                        if (partial.ToLower().IndexOf("multiple locations") != -1)
                        {
                            i = fullAddress.IndexOf("?f=q&");
                            continue;
                        }

                        i = partial.IndexOf("&sll=");
                        i1 = partial.IndexOf(',', i);
                        i += 5;

                        // get latitude
                        dd.data[11] = partial.Substring(i, i1 - i);
                        RemoveSpaces(ref dd.data[11], true, true);

                        // get longitude
                        int i2 = partial.IndexOf('&', i1);
                        dd.data[12] = partial.Substring(i1, i2 - i1);
                        RemoveSpaces(ref dd.data[12], true, true);

                        partial = partial.Remove(i - 5, partial.Length - (i - 5));

                        if (partial.Substring(0, 5) == "%loca")
                        {
                            i = partial.IndexOf("?f=q&");
                            aux1 = partial.Substring(5, i - 5);

                            i = partial.IndexOf(aux1, i);
                            i += aux1.Length;
                            partial = partial.Remove(0, i);
                            RemoveSpaces(ref partial, true, true);

                            if (aux1.Contains('@'))
                            {
                                i1 = aux1.IndexOf('@');
                                aux1.Remove(i1);
                            }
                            RemoveSpaces(ref aux1, true, true);
                            dealer_address.SetStreetAddress(aux1);

                            if ((partial[partial.Length - 1] >= '0') && (partial[partial.Length - 1] <= '9'))
                            { // Complete postal code
                                i = partial.LastIndexOf(' ');
                                if (partial.Length - i < 6)
                                    i = partial.LastIndexOf(' ', i - 1);
                                aux1 = partial.Substring(i, partial.Length - i);
                                partial = partial.Remove(i);
                                RemoveSpaces(ref aux1, true, true);
                                dealer_address.SetPostalCode(aux1);
                            }
                            else if ((partial[partial.Length - 2] >= '0') && (partial[partial.Length - 2] <= '9'))
                            { // Partial postal code
                                i = partial.LastIndexOf(' ');
                                aux1 = partial.Substring(i, partial.Length - i);
                                partial = partial.Remove(i);
                                RemoveSpaces(ref aux1, true, true);
                                dealer_address.SetPostalCode(aux1);
                            }
                            i = partial.LastIndexOf(' ');
                            if (i == -1)
                                i = 0;
                            aux1 = partial.Substring(i, partial.Length - i);
                            RemoveSpaces(ref aux1, true, true);
                            if (aux1.Length > 2)
                            {

                                if ((aux1.ToLower() == "alberta") || (aux1.ToLower() == "manitoba") || (aux1.ToLower() == "ontario") || (aux1.ToLower() == "quebec") || (aux1.ToLower() == "saskatchewan") || (aux1.ToLower() == "nunavut") || (aux1.ToLower() == "yukon") ||
                                    (aux1.ToLower() == "alabama") || (aux1.ToLower() == "alaska") || (aux1.ToLower() == "arizona") || (aux1.ToLower() == "arkansas") ||
                                    (aux1.ToLower() == "california") || (aux1.ToLower() == "colorado") || (aux1.ToLower() == "connecticut") || (aux1.ToLower() == "delaware") ||
                                    (aux1.ToLower() == "florida") || (aux1.ToLower() == "georgia") || (aux1.ToLower() == "hawaii") || (aux1.ToLower() == "idaho") ||
                                    (aux1.ToLower() == "illinois") || (aux1.ToLower() == "indiana") || (aux1.ToLower() == "iowa") || (aux1.ToLower() == "kansas") ||
                                    (aux1.ToLower() == "kentucky") || (aux1.ToLower() == "louisiana") || (aux1.ToLower() == "maine") || (aux1.ToLower() == "maryland") ||
                                    (aux1.ToLower() == "massachusetts") || (aux1.ToLower() == "michigan") || (aux1.ToLower() == "minnesota") || (aux1.ToLower() == "mississippi") ||
                                    (aux1.ToLower() == "missouri") || (aux1.ToLower() == "montana") || (aux1.ToLower() == "nebraska") || (aux1.ToLower() == "nevada") ||
                                    (aux1.ToLower() == "ohio") || (aux1.ToLower() == "oklahoma") || (aux1.ToLower() == "oregon") || (aux1.ToLower() == "pennsylvania") ||
                                    (aux1.ToLower() == "tennessee") || (aux1.ToLower() == "texas") || (aux1.ToLower() == "utah") || (aux1.ToLower() == "vermont") ||
                                    (aux1.ToLower() == "virginia") || (aux1.ToLower() == "washington") || (aux1.ToLower() == "wisconsin") || (aux1.ToLower() == "wyoming"))
                                {
                                    partial = partial.Remove(i);
                                    dealer_address.SetProvince(aux1);
                                }
                                else if (i > 0)
                                {
                                    i = partial.LastIndexOf(' ', i - 1);
                                    if (i == -1)
                                        i = 0;
                                    aux1 = partial.Substring(i, partial.Length - i);
                                    RemoveSpaces(ref aux1, true, true);
                                    if ((aux1.ToLower() == "british columbia") || (aux1.ToLower() == "new brunswick") || (aux1.ToLower() == "nova scotia") || (aux1.ToLower() == "northwest territories") ||
                                    (aux1.ToLower() == "new hampshire") || (aux1.ToLower() == "new jersey") || (aux1.ToLower() == "new mexico") || (aux1.ToLower() == "new york") ||
                                    (aux1.ToLower() == "north carolina") || (aux1.ToLower() == "north dakota") || (aux1.ToLower() == "rhode island") || (aux1.ToLower() == "south carolina") ||
                                    (aux1.ToLower() == "south dakota") || (aux1.ToLower() == "west virginia"))
                                    {
                                        partial = partial.Remove(i);
                                        dealer_address.SetProvince(aux1);
                                    }
                                    else if (i > 0)
                                    {
                                        i = partial.LastIndexOf(' ');
                                        if (i == -1)
                                            i = 0;
                                        aux1 = partial.Substring(i, partial.Length - i);
                                        RemoveSpaces(ref aux1, true, true);
                                        if ((aux1.ToLower() == "newfoundland and labrador") || (aux1.ToLower() == "prince edward island"))
                                        {
                                            partial = partial.Remove(i);
                                            dealer_address.SetProvince(aux1);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                partial = partial.Remove(i);
                                dealer_address.SetProvince(getProvince(aux1, ref dd.data[17]));
                            }
                            RemoveSpaces(ref partial, true, true);
                            dealer_address.SetCity(partial);
                        }
                        else 
                        { // it is %city
                            i = partial.IndexOf("?f=q&");
                            aux1 = partial.Substring(5, i - 5);
                            RemoveSpaces(ref aux1, true, true);
                            dealer_address.SetCity(aux1);

                            i = partial.LastIndexOf(aux1);
                            i1 = partial.LastIndexOf("&q=", i);
                            i1 += 3;

                            aux2 = partial.Substring(i1, i - i1);
                            RemoveSpaces(ref aux2, true, true);
                            dealer_address.SetStreetAddress(aux2);

                            i += aux1.Length;
                            partial = partial.Remove(0, i);
                            RemoveSpaces(ref partial, true, true);

                            if ((partial[partial.Length - 1] >= '0') && (partial[partial.Length - 1] <= '9'))
                            { // Complete postal code
                                i = partial.LastIndexOf(' ');
                                if ((partial.Length - i) < 5)
                                    i = partial.LastIndexOf(' ', i - 1);
                                aux1 = partial.Substring(i, partial.Length - i);
                                partial = partial.Remove(i);
                                RemoveSpaces(ref aux1, true, true);
                                dealer_address.SetPostalCode(aux1);
                                RemoveSpaces(ref partial, true, true);
                            }
                            else if ((partial[partial.Length - 2] >= '0') && (partial[partial.Length - 2] <= '9'))
                            { // Partial postal code
                                i = partial.LastIndexOf(' ');
                                aux1 = partial.Substring(i, partial.Length - i);
                                partial = partial.Remove(i);
                                RemoveSpaces(ref aux1, true, true);
                                dealer_address.SetPostalCode(aux1);
                                RemoveSpaces(ref partial, true, true);
                            }
                            if (partial.Length > 2)
                                dealer_address.SetProvince(partial);
                            else
                                dealer_address.SetProvince(getProvince(partial, ref dd.data[17]));
                        }

                        dealer_address.SetCountry(dd.data[17]);

                        if ((dd.data[11] == "") && (dd.data[12] == ""))
                        {
                            GetLatLong(dealer_address, dd);
                        }
                        else
                        {
                            RoundLatLong(ref dd.data[11], ref dd.data[12], ref AtTheEnd, dd.data[5]);
                            dealer_address.SetLatitude(dd.data[11]);
                            dealer_address.SetLongitude(dd.data[12]);
                        }
                        dealer_address.SetMap(CreateMapLink(dd));

                        if (dd.data[19] != "")
                        { // contacts can have more than one, like phones and emails
                            aux1 = dealer_address.GetContact();
                            do
                            {
                                i = dd.data[19].LastIndexOf(";");
                                if (i == -1)
                                    i = 0;
                                string aux = dd.data[19].Substring(i, dd.data[19].Length - i);
                                dd.data[19] = dd.data[19].Remove(i);
                                RemoveSpaces(ref aux, true, true);
                                if (aux1 == "")
                                    aux1 = aux;
                                else if (!aux1.Contains(aux))
                                {
                                    aux1 = aux1 + "; " + aux;
                                }
                            } while (dd.data[19] != "");
                            dealer_address.SetContact(aux1);
                        }

                        if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                        {
                            if (dd.data[6].ToLower() != "mobile")
                            {
                                dealer_address.SetCity("");
                                dealer_address.SetProvince("");
                                dealer_address.SetCountry("");
                            }
                            dealer_address.SetLatitude("");
                            dealer_address.SetLongitude("");
                            dealer_address.SetMap("");
                        }
                        else
                        {
                            if (dealer_address.GetStreetAddress() == "")
                                dealer_address.SetStreetAddress(dd.data[14]);
                            if (dealer_address.GetCity() == "")
                                dealer_address.SetCity(dd.data[15]);
                            if (dealer_address.GetPostalCode() == "")
                                dealer_address.SetPostalCode(dd.data[16]);
                            if (dealer_address.GetCountry() == "")
                                dealer_address.SetCountry(dd.data[17]);
                            if (dealer_address.GetProvince() == "")
                                dealer_address.SetProvince(dd.data[43]);
                        }

                        gotAtLeastOne = true;

                        locations.SetLocation(dealer_address);

                        i = fullAddress.IndexOf("?f=q&");
                    }
                }
                else
                {
                    i = fullAddress.IndexOf("&sll=");
                    while (i != -1)
                    {
                        int i1 = fullAddress.IndexOf(';', i);
                        if (i1 == -1)
                            i1 = fullAddress.Length;

                        fullAddress = fullAddress.Remove(i, i1 - i);
                        i = fullAddress.IndexOf("&sll=");
                    }
                    fullAddress = fullAddress.Replace('+', ' ');
                    fullAddress = fullAddress.Replace(";", ";;");
                    RemoveSpaces(ref fullAddress, true, true);

                }

                string contact = dd.data[19];

                fullAddress = fullAddress.Replace("; ;", ";;");
                if (fullAddress.Contains(";;;"))
                {
                    fullAddress = fullAddress.Replace(";;", ";");
                    while (fullAddress.Contains(";;;"))
                        fullAddress = fullAddress.Replace(";;;", ";;");
                }

                while (fullAddress != "")
                {
                    RemoveSpaces(ref fullAddress, true, true);
                    location dealer_address = new location();
                    Boolean got = false;
                    i = fullAddress.IndexOf(";;");
                    if (i == -1)
                        i = fullAddress.Length;
                    aux1 = fullAddress.Substring(0, i);
                    RemoveSpaces(ref aux1, true, true);
                    fullAddress = fullAddress.Remove(0, i);

                    got = GetAddressFromGoogleMaps(ref aux1, ref dealer_address, dd.data[5], ref timesCalled, ref timesGoogleMaps, dd.data[43]);

                    if (got)
                    {
                        gotAtLeastOne = true;

                        aux1 = aux1.Replace(':', ';');
                        while (aux1 != "") // look for phone information in aux1
                        {
                            Boolean isPhone = true;
                            RemoveSpaces(ref aux1, true, true);

                            i = aux1.LastIndexOf(';');
                            if (i == -1)
                                i = 0;
                            string aux3 = aux1.Substring(i, aux1.Length - i);
                            aux1 = aux1.Remove(i);
                            RemoveSpaces(ref aux3, true, true);

                            aux2 = aux3.Replace(" ", "");
                            aux2 = aux2.Replace("(", "");
                            aux2 = aux2.Replace(")", "");
                            aux2 = aux2.Replace("-", "");
                            aux2 = aux2.Replace(".", "");

                            if (((aux2.Length >= 6) && (aux2.Length <= 12)) || ((aux2.Length >= 20) && (aux2.Length <= 24)))
                            {
                                try
                                {
                                    // if first 6 digits are number, it is considered a phone number
                                    int test = Convert.ToInt32(aux2.Substring(0, 6));
                                }
                                catch (FormatException)
                                {
                                    try
                                    {
                                        int test = Convert.ToInt32(aux2.Substring(0, 3));
                                        test = Convert.ToInt32(aux2.Substring(aux2.Length - 3, 3));
                                    }
                                    catch (FormatException)
                                    {
                                        isPhone = false;
                                    }
                                }
                            }
                            else
                                isPhone = false;

                            if (isPhone)
                            {
                                dealer_address.SetContact(aux3);
                                aux1 = "";
                            }
                        }
                        if (dd.data[19] != "")
                        { // contacts can have more than one, like phones and emails
                            aux1 = dealer_address.GetContact();
                            do
                            {
                                i = dd.data[19].LastIndexOf(";");
                                if (i == -1)
                                    i = 0;
                                string aux = dd.data[19].Substring(i, dd.data[19].Length - i);
                                dd.data[19] = dd.data[19].Remove(i);
                                RemoveSpaces(ref aux, true, true);
                                if (aux1 == "")
                                    aux1 = aux;
                                else if (!aux1.Contains(aux))
                                {
                                    aux1 = aux1 + "; " + aux;
                                }
                            } while (dd.data[19] != "");
                            dealer_address.SetContact(aux1);
                        }

                        if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                        {
                            if (dd.data[6].ToLower() != "mobile")
                            {
                                dealer_address.SetCity("");
                                dealer_address.SetProvince("");
                                dealer_address.SetCountry("");
                            }
                            dealer_address.SetLatitude("");
                            dealer_address.SetLongitude("");
                            dealer_address.SetMap("");
                        }
                        else
                        {
                            if (dealer_address.GetStreetAddress() == "")
                                dealer_address.SetStreetAddress(dd.data[14]);
                            if (dealer_address.GetCity() == "")
                                dealer_address.SetCity(dd.data[15]);
                            if (dealer_address.GetPostalCode() == "")
                                dealer_address.SetPostalCode(dd.data[16]);
                            if (dealer_address.GetCountry() == "")
                                dealer_address.SetCountry(dd.data[17]);
                            if (dealer_address.GetProvince() == "")
                                dealer_address.SetProvince(dd.data[43]);
                        }
                        locations.SetLocation(dealer_address);
                    }
                }
                if (!gotAtLeastOne)
                {
                    dd.data[13] = "#2#";
                    // in case there are data in 14, 15, 16, 17, 19 and/or 43, get them
                    return (evalutatingFullAddress(dd, ref timesCalled, ref timesGoogleMaps));
                }




                /*                while (fullAddress != "")
                                {
                                    location dealer_address = new location();

                                    i = fullAddress.LastIndexOf('-');
                                    if (i != -1)
                                    {
                                        aux1 = fullAddress.Substring(i + 1, fullAddress.Length - (i+1));
                                        RemoveSpaces(ref aux1, true, true);
                                        dealer_address.SetContact(aux1);
                                        fullAddress = fullAddress.Remove(i);
                                    }

                                    i = fullAddress.LastIndexOf(':');
                                    if (i != -1)
                                    {
                                        aux1 = fullAddress.Substring(i + 1, fullAddress.Length - i);
                                        RemoveSpaces(ref aux1, true, true);
                                        dealer_address.SetStreetAddress(aux1);
                                        fullAddress = fullAddress.Remove(i);
                                    }

                                    i = fullAddress.LastIndexOf(';');
                                    if (i == -1)
                                        i = 0;
                                    aux1 = fullAddress.Substring(i, fullAddress.Length - i);
                                    RemoveSpaces(ref aux1, true, true);
                                    dealer_address.SetCity(aux1);
                                    fullAddress = fullAddress.Remove(i);

                                    dealer_address.SetCountry(dd.data[17]);

                                    GetLatLong(dealer_address, dd);
                                    dealer_address.SetMap(CreateMapLink(dd));

                                    if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                                    {
                                        dealer_address.SetCity("");
                                        dealer_address.SetProvince("");
                                        dealer_address.SetCountry("");
                                        dealer_address.SetLatitude("");
                                        dealer_address.SetLongitude("");
                                        dealer_address.SetMap("");
                                    }
                                    else
                                    {
                                        if (dealer_address.GetStreetAddress() == "")
                                            dealer_address.SetStreetAddress(dd.data[14]);
                                        if (dealer_address.GetCity() == "")
                                            dealer_address.SetCity(dd.data[15]);
                                        if (dealer_address.GetPostalCode() == "")
                                            dealer_address.SetPostalCode(dd.data[16]);
                                        if (dealer_address.GetCountry() == "")
                                            dealer_address.SetCountry(dd.data[17]);
                                        if (dd.data[19] != "")
                                        { // contacts can have more than one, like phones and emails
                                            aux1 = dealer_address.GetContact();
                                            do
                                            {
                                                i = dd.data[19].LastIndexOf(";");
                                                if (i == -1)
                                                    i = 0;
                                                string aux = dd.data[19].Substring(i, dd.data[19].Length - i);
                                                dd.data[19] = dd.data[19].Remove(i);
                                                RemoveSpaces(ref aux, true, true);
                                                if (aux1 == "")
                                                    aux1 = aux;
                                                else if (!aux1.Contains(aux))
                                                {
                                                    aux1 = aux1 + "; " + aux;
                                                }
                                            } while (dd.data[19] != "");
                                            dealer_address.SetContact(aux1);
                                        }
                                        if (dealer_address.GetProvince() == "")
                                            dealer_address.SetProvince(dd.data[43]);
                                    }

                                    locations.SetLocation(dealer_address);

                                    RemoveSpaces(ref fullAddress, true, true);
                                }*/
            }

            if (format == 5) // 2.	street; City, [Province] [Postal Code]; [Contact] | street; [Postal Code] City; [Contact] && Lat/Longit from dd.data[18] - maps (International - . Ex: LivingSocial) 
            {
                List<string> countryList1 = new List<string>();
                List<string> countryList2 = new List<string>();

                countryList1.Add("Netherlands");

                countryList2.Add("Canada");
                countryList2.Add("United States");
                countryList2.Add("New Zealand");
                countryList2.Add("Australia");

                string fullAddress = dd.data[13];
                string aux1, aux2;
                dd.data[13] = "";

                RemoveSpaces(ref fullAddress, true, true);
                fullAddress = fullAddress.Replace(";;\r;", "|");
                fullAddress = fullAddress.Replace("\r", ";");
                while (fullAddress.Contains(";;"))
                    fullAddress = fullAddress.Replace(";;", ";");
                while (fullAddress.Contains("|;"))
                    fullAddress = fullAddress.Replace("|;", "|");

                while (fullAddress.Length > 0)
                {
                    Boolean isPhone = true;
                    location dealer_address = new location();
                        
                    RemoveSpaces(ref fullAddress, true, true);

                    int i = fullAddress.LastIndexOf(';');
                    if (i == -1)
                        i = 0;

                    aux1 = fullAddress.Substring(i, fullAddress.Length - i);
                    RemoveSpaces(ref aux1, true, true);
                    
                    aux2 = aux1.Replace(" ","");

                    aux2 = aux2.Replace("(","");
                    aux2 = aux2.Replace(")","");
                    aux2 = aux2.Replace("-","");
                    aux2 = aux2.Replace("−","");
                    aux2 = aux2.Replace(".","");
                    aux2 = aux2.Replace("_","");

                    if ((aux2.Length >= 6) || ((dd.data[17] == "Egypt") && (aux2.Length >= 5)))
                    {
                        try
                        {
                            int test;
                            // if first 6 digits are number (or 5 for Egypt), it is considered a phone number
                            if (dd.data[17] == "Egypt")
                                test = Convert.ToInt32(aux2.Substring(0, 5));
                            else
                                test = Convert.ToInt32(aux2.Substring(0, 6));
                        }
                        catch (FormatException)
                        {
//                            try
//                            {
//                                int test = Convert.ToInt32(aux2.Substring(0, 3));
//                                test = Convert.ToInt32(aux2.Substring(aux2.Length - 3, 3));
//                            }
//                            catch (FormatException)
//                            {
                                try
                                {
                                    int test = Convert.ToInt32(aux2.Substring(0, 3));
                                    if ((aux1[3] != '-') || (aux1[7] != '-'))
                                        isPhone = false;
                                }
                                catch (FormatException)
                                {
                                    isPhone = false;
                                }
//                            }
                        }
                    }
                    else
                        isPhone = false;

                    if (isPhone)
                    {
                        fullAddress = fullAddress.Remove(i);
                        dealer_address.SetContact(aux1);
                    }
                    else if (dd.data[17] == "Ireland") // have to do that, to avoid making lots of changes in the code. It only happens in Ireland if there is no phone. That's because, with the phone, there is a comma between city and phone. If address comes without phone, the comma is removed automatically. So, It was included again.
                        fullAddress = fullAddress + ',';

                    if (fullAddress.Length > 0)
                    {
                        i = fullAddress.LastIndexOf(';');
                        if (i == -1)
                            i = 0;
                        else
                        {
                            string temp = fullAddress.Substring(i, fullAddress.Length - i);
                            RemoveSpaces(ref temp, true, true);
                            if (temp.Length <= 1)
                            {
                                fullAddress = fullAddress.Remove(i);
                                RemoveSpaces(ref fullAddress, true, true);
                                i = fullAddress.LastIndexOf(';');
                                if (i == -1)
                                    i = 0;
                            }
                        }

                        int i1 = fullAddress.LastIndexOf('|');
                        if (i1 == -1)
                            i1 = 0;
                        if (i1 >= i)
                        {
                            fullAddress = fullAddress.Remove(i1);
                            continue;
                        }

                        i1 = fullAddress.LastIndexOf(',');
                        if (i1 == -1)
                            i1 = 0;

                        if (i > i1)
                        {
                            aux1 = fullAddress.Substring(i, fullAddress.Length - i);
                            RemoveSpaces(ref aux1, true, true);

                            int sep = aux1.IndexOf(' ');
                            if (sep == -1)
                                sep = aux1.Length;
//                            int sep2 = -1;
                            if (countryList1.Contains(dd.data[17]))
                            {
                                if (sep + 1 < aux1.Length)
                                {
                                    int sep1 = aux1.IndexOf(' ', sep + 1);
                                    if (sep1 != -1)
                                        sep = sep1;
//                                   sep2 = aux1.IndexOf(' ', sep + 1);
                                }
                            }
//                            if (sep2 != -1)
//                                aux2 = aux1.Substring(sep2, aux1.Length - sep2);
//                            else
                                aux2 = aux1.Substring(sep, aux1.Length - sep);

                            RemoveSpaces(ref aux2, true, true);
                            dealer_address.SetCity(aux2);

                            aux2 = aux1.Substring(0, sep);
                            RemoveSpaces(ref aux2, true, true);
                            dealer_address.SetPostalCode(aux2);
                        }
                        else
                        {
                            aux1 = fullAddress.Substring(i1, fullAddress.Length - i1);
                            RemoveSpaces(ref aux1, true, true);
                            if (aux1 != "")
                            {
                                if (countryList2.Contains(dd.data[17]))
                                {
                                    int sep = aux1.IndexOf(' ');
                                    if (sep == -1)
                                    {
                                        aux2 = aux1;
                                        RemoveSpaces(ref aux2, true, true);
                                        if (aux2.Length > 2)
                                        {
                                            if ((aux2.ToLower() == "alberta") || (aux2.ToLower() == "manitoba") || (aux2.ToLower() == "ontario") ||
                                                (aux2.ToLower() == "quebec") || (aux2.ToLower() == "saskatchewan") || (aux2.ToLower() == "nunavut") || (aux2.ToLower() == "yukon") ||
                                                (aux2.ToLower() == "alabama") || (aux2.ToLower() == "alaska") || (aux2.ToLower() == "arizona") || (aux2.ToLower() == "arkansas") ||
                                                (aux2.ToLower() == "california") || (aux2.ToLower() == "colorado") || (aux2.ToLower() == "connecticut") || (aux2.ToLower() == "delaware") ||
                                                (aux2.ToLower() == "florida") || (aux2.ToLower() == "georgia") || (aux2.ToLower() == "hawaii") || (aux2.ToLower() == "idaho") ||
                                                (aux2.ToLower() == "illinois") || (aux2.ToLower() == "indiana") || (aux2.ToLower() == "iowa") || (aux2.ToLower() == "kansas") ||
                                                (aux2.ToLower() == "kentucky") || (aux2.ToLower() == "louisiana") || (aux2.ToLower() == "maine") || (aux2.ToLower() == "maryland") ||
                                                (aux2.ToLower() == "massachusetts") || (aux2.ToLower() == "michigan") || (aux2.ToLower() == "minnesota") || (aux2.ToLower() == "mississippi") ||
                                                (aux2.ToLower() == "missouri") || (aux2.ToLower() == "montana") || (aux2.ToLower() == "nebraska") || (aux2.ToLower() == "nevada") ||
                                                (aux2.ToLower() == "ohio") || (aux2.ToLower() == "oklahoma") || (aux2.ToLower() == "oregon") || (aux2.ToLower() == "pennsylvania") ||
                                                (aux2.ToLower() == "tennessee") || (aux2.ToLower() == "texas") || (aux2.ToLower() == "utah") || (aux2.ToLower() == "vermont") ||
                                                (aux2.ToLower() == "virginia") || (aux2.ToLower() == "washington") || (aux2.ToLower() == "wisconsin") || (aux2.ToLower() == "wyoming") ||
                                                (aux2.ToLower() == "british columbia") || (aux2.ToLower() == "new brunswick") || (aux2.ToLower() == "nova scotia") || (aux2.ToLower() == "northwest territories") ||
                                                (aux2.ToLower() == "new hampshire") || (aux2.ToLower() == "new jersey") || (aux2.ToLower() == "new mexico") || (aux2.ToLower() == "new york") ||
                                                (aux2.ToLower() == "north carolina") || (aux2.ToLower() == "north dakota") || (aux2.ToLower() == "rhode island") || (aux2.ToLower() == "south carolina") ||
                                                (aux2.ToLower() == "south dakota") || (aux2.ToLower() == "west virginia") ||
                                                (aux2.ToLower() == "newfoundland and labrador") || (aux2.ToLower() == "newfoundland") || (aux2.ToLower() == "prince edward island"))
                                            {
                                                dealer_address.SetProvince(aux2);
                                            }
                                            else
                                                dealer_address.SetPostalCode(aux2);
                                        }
                                        else
                                        {
                                            dealer_address.SetProvince(getProvince(aux2, ref dd.data[17]));
                                        }
                                    }
                                    else
                                    {
                                        string part = aux1 .Substring(0 , sep);
                                        RemoveSpaces(ref part, true, true);
//                                        if ((part.Length > 2) && (part.ToLower() != "d.c"))
                                        if ((part.Length > 3) || (part.ToLower() == "new"))
                                        {
                                            if ((part.ToLower() == "alberta") || (part.ToLower() == "manitoba") || (part.ToLower() == "ontario") || (part.ToLower() == "quebec") || (part.ToLower() == "saskatchewan") || (part.ToLower() == "nunavut") || (part.ToLower() == "yukon") || (part.ToLower() == "newfoundland") || 
                                                (part.ToLower() == "alabama") || (part.ToLower() == "alaska") || (part.ToLower() == "arizona") || (part.ToLower() == "arkansas") ||
                                                (part.ToLower() == "california") || (part.ToLower() == "colorado") || (part.ToLower() == "connecticut") || (part.ToLower() == "delaware") ||
                                                (part.ToLower() == "florida") || (part.ToLower() == "georgia") || (part.ToLower() == "hawaii") || (part.ToLower() == "idaho") ||
                                                (part.ToLower() == "illinois") || (part.ToLower() == "indiana") || (part.ToLower() == "iowa") || (part.ToLower() == "kansas") ||
                                                (part.ToLower() == "kentucky") || (part.ToLower() == "louisiana") || (part.ToLower() == "maine") || (part.ToLower() == "maryland") ||
                                                (part.ToLower() == "massachusetts") || (part.ToLower() == "michigan") || (part.ToLower() == "minnesota") || (part.ToLower() == "mississippi") ||
                                                (part.ToLower() == "missouri") || (part.ToLower() == "montana") || (part.ToLower() == "nebraska") || (part.ToLower() == "nevada") ||
                                                (part.ToLower() == "ohio") || (part.ToLower() == "oklahoma") || (part.ToLower() == "oregon") || (part.ToLower() == "pennsylvania") ||
                                                (part.ToLower() == "tennessee") || (part.ToLower() == "texas") || (part.ToLower() == "utah") || (part.ToLower() == "vermont") ||
                                                (part.ToLower() == "virginia") || (part.ToLower() == "washington") || (part.ToLower() == "wisconsin") || (part.ToLower() == "wyoming"))
                                            {
                                                if (part.ToLower() == "newfoundland")
                                                {
                                                    if (aux1.ToLower().Contains("labrador"))
                                                    {
                                                        sep = aux1.IndexOf("labrador") + "labrador".Length;
                                                    }
                                                }
                                                aux1 = aux1.Remove(0, sep);
                                                dealer_address.SetProvince(part);

                                                RemoveSpaces(ref aux1, true, true);
                                                dealer_address.SetPostalCode(aux1);
                                            }
                                            else if (sep + 1 < aux1.Length)
                                            {
                                                sep = aux1.IndexOf(' ', sep + 1);
                                                if (sep == -1)
                                                    sep = aux1.Length;
                                                part = aux1.Substring(0, sep);
                                                RemoveSpaces(ref part, true, true);
                                                if ((part.ToLower() == "british columbia") || (part.ToLower() == "new brunswick") || (part.ToLower() == "nova scotia") || (part.ToLower() == "northwest territories") ||
                                                (part.ToLower() == "new hampshire") || (part.ToLower() == "new jersey") || (part.ToLower() == "new mexico") || (part.ToLower() == "new york") ||
                                                (part.ToLower() == "north carolina") || (part.ToLower() == "north dakota") || (part.ToLower() == "rhode island") || (part.ToLower() == "south carolina") ||
                                                (part.ToLower() == "south dakota") || (part.ToLower() == "west virginia"))
                                                {
                                                    aux1 = aux1.Remove(0, sep);
                                                    dealer_address.SetProvince(part);

                                                    RemoveSpaces(ref aux1, true, true);
                                                    dealer_address.SetPostalCode(aux1);
                                                }
                                                else if (sep + 1 < aux1.Length)
                                                {
                                                    sep = aux1.IndexOf(' ');
                                                    if (sep == -1)
                                                        sep = aux1.Length;
                                                    part = aux1.Substring(0, sep);
                                                    RemoveSpaces(ref part, true, true);
                                                    if ((part.ToLower() == "newfoundland and labrador") || (part.ToLower() == "prince edward island"))
                                                    {
                                                        aux1 = aux1.Remove(0, sep);
                                                        dealer_address.SetProvince(part);

                                                        RemoveSpaces(ref aux1, true, true);
                                                        dealer_address.SetPostalCode(aux1);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            aux2 = aux1.Substring(sep, aux1.Length - sep);
                                            RemoveSpaces(ref aux2, true, true);
                                            dealer_address.SetPostalCode(aux2);

//                                            aux2 = aux1.Substring(0, sep);
//                                            RemoveSpaces(ref aux2, true, true);
                                            dealer_address.SetProvince(getProvince(part, ref dd.data[17]));
                                        }
                                    }
                                }
                                else
                                {
                                    if (dd.data[17] == "United Kingdom")
                                    {
                                        int sep = aux1.IndexOf(' ');

                                        if ((sep == -1) || (aux1.Length - sep < 6))
                                        {
                                            dealer_address.SetPostalCode(aux1);
                                        }
                                        else
                                        {
                                            aux2 = aux1.Substring(sep, aux1.Length - sep);
                                            RemoveSpaces(ref aux2, true, true);
                                            dealer_address.SetPostalCode(aux2);

                                            aux2 = aux1.Substring(0, sep);
                                            RemoveSpaces(ref aux2, true, true);
                                            dealer_address.SetProvince(getProvince(aux2, ref dd.data[17]));
                                        }
                                    }
                                    else
                                        dealer_address.SetPostalCode(aux1);
                                }
                            }
                            int i2 = fullAddress.LastIndexOf(',', i1 - 1);
                            if ((i2 != -1) && (i2 > i))
                            {
                                i = i2;
                            }
                            aux1 = fullAddress.Substring(i, i1 - i);
                            RemoveSpaces(ref aux1, true, true);
                            dealer_address.SetCity(aux1);
                        }
                        fullAddress = fullAddress.Remove(i);

                        i = fullAddress.LastIndexOf('|');
                        if (i == -1)
                            i = 0;
                        aux1 = fullAddress.Substring(i, fullAddress.Length - i);
                        RemoveSpaces(ref aux1, true, true);
                        dealer_address.SetStreetAddress(aux1);
                        fullAddress = fullAddress!=""?fullAddress.Remove(i):"";
                        
                        if ((fullAddress.Length == 0) && (locations.CountLocations() == 0))
                        {
                            if (dealer_address.GetProvince() == "")
                            {
                                RemoveSpaces(ref dd.data[43], true, true);
                                if (dd.data[43] != "")
                                {
                                    dd.data[43] = getProvince(dd.data[43], ref dd.data[17]);
                                    dealer_address.SetProvince(dd.data[43]);
                                }
                            }
                            if ((dd.data[11] == "") && (dd.data[12] == ""))
                            {
                                GetLatLong(dealer_address, dd);
                            }
                            else
                            {
                                RoundLatLong(ref dd.data[11], ref dd.data[12], ref AtTheEnd, dd.data[5]);
                                dealer_address.SetLatitude(dd.data[11]);
                                dealer_address.SetLongitude(dd.data[12]);
                            }
                            dealer_address.SetMap(CreateMapLink(dd));
                        }
                        else
                        {
                            // Get Latitude and Longitude from dd.data[18]
                            i = dd.data[18].LastIndexOf('|');
                            if (i == -1)
                                i = 0;
                            aux1 = dd.data[18].Substring(i, dd.data[18].Length - i);
                            dd.data[18] = dd.data[18] != ""?dd.data[18].Remove(i):"";
                            RemoveSpaces(ref aux1, true, true);

                            i = aux1.IndexOf('=');
                            i1 = i!=-1?aux1.IndexOf(',', i):-1;

                            if ((i != -1) && (i1 != -1))
                            {
                                aux2 = aux1.Substring(i + 1, i1 - i);
                                RemoveSpaces(ref aux2, true, true);
                                dd.data[11] = aux2;

                                aux2 = aux1.Substring(i1, aux1.Length - i1);
                                RemoveSpaces(ref aux2, true, true);
                                dd.data[12] = aux2;

                                RoundLatLong(ref dd.data[11], ref dd.data[12], ref AtTheEnd, dd.data[5]);
                                dealer_address.SetLatitude(dd.data[11]);
                                dealer_address.SetLongitude(dd.data[12]);
                            }
                            else
                            {
                                GetLatLong(dealer_address, dd);
                            } 
                            dealer_address.SetMap(CreateMapLink(dd));
                        }

                        dealer_address.SetCountry(dd.data[17]);

                        if (dd.data[19] != "")
                        { // contacts can have more than one, like phones and emails
                            aux1 = dealer_address.GetContact();
                            do
                            {
                                i = dd.data[19].LastIndexOf(";");
                                if (i == -1)
                                    i = 0;
                                string aux = dd.data[19].Substring(i, dd.data[19].Length - i);
                                dd.data[19] = dd.data[19].Remove(i);
                                RemoveSpaces(ref aux, true, true);
                                if (aux1 == "")
                                    aux1 = aux;
                                else if (!aux1.Contains(aux))
                                {
                                    aux1 = aux1 + "; " + aux;
                                }
                            } while (dd.data[19] != "");
                            dealer_address.SetContact(aux1);
                        }

                        if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                        {
                            if (dd.data[6].ToLower() != "mobile")
                            {
                                dealer_address.SetCity("");
                                dealer_address.SetProvince("");
                                dealer_address.SetCountry("");
                            }
                            dealer_address.SetLatitude("");
                            dealer_address.SetLongitude("");
                            dealer_address.SetMap("");
                        }
                        else 
                        {
                            if (dealer_address.GetStreetAddress() == "")
                                dealer_address.SetStreetAddress(dd.data[14]);
                            if (dealer_address.GetCity() == "")
                                dealer_address.SetCity(dd.data[15]);
                            if (dealer_address.GetPostalCode() == "")
                                dealer_address.SetPostalCode(dd.data[16]);
                            if (dealer_address.GetCountry() == "")
                                dealer_address.SetCountry(dd.data[17]);
                            if (dealer_address.GetProvince() == "")
                                dealer_address.SetProvince(dd.data[43]);
                        }
                    }
                    
                    locations.SetLocation(dealer_address);
//                    dd.data[13] = dd.data[13] != "" ? (dd.data[13] + ", ") : "";
//                    dd.data[13] += dealer_address.GetStreetAddress() != "" ? (dealer_address.GetStreetAddress() + ", ") : "";
//                    dd.data[13] += dealer_address.GetCity() != "" ? (dealer_address.GetCity() + ", ") : "";
//                    dd.data[13] += dealer_address.GetProvince() != "" ? (dealer_address.GetProvince() + ", ") : "";
//                    dd.data[13] += dealer_address.GetCountry() != "" ? (dealer_address.GetCountry() + ", ") : "";
//                    dd.data[13] += dealer_address.GetPostalCode() != "" ? (dealer_address.GetPostalCode() + ", ") : "";
//                    dd.data[13] += dealer_address.GetContact() != "" ? dealer_address.GetContact() : "";
//                    RemoveSpaces(ref dd.data[13], true, true);
                }
//                return locations;
            }
            if (format == 6)
            {
                string fullAddress = dd.data[13];
                string aux1, aux2;
                dd.data[13] = "";
                int i;
                Boolean isPhone = true;
                location dealer_address = new location();

                while (fullAddress != "")
                {
                    RemoveSpaces(ref fullAddress, true, true);

                    i = fullAddress.LastIndexOf(';');
                    if (i == -1)
                        i = 0;

                    aux1 = fullAddress.Substring(i, fullAddress.Length - i);
                    RemoveSpaces(ref aux1, true, true);

                    aux2 = aux1.Replace(" ", "");
                    aux2 = aux2.Replace("(", "");
                    aux2 = aux2.Replace(")", "");
                    aux2 = aux2.Replace("-", "");
                    aux2 = aux2.Replace(".", "");
                    aux2 = aux2.Replace("_", "");

                    if (aux2.Length >= 6)
                    {
                        try
                        {
                            // if first 6 digits are number, it is considered a phone number
                            int test = Convert.ToInt32(aux2.Substring(0, 6));
                        }
                        catch (FormatException)
                        {
                            try
                            {
                                int test = Convert.ToInt32(aux2.Substring(0, 3));
                                test = Convert.ToInt32(aux2.Substring(aux2.Length - 3, 3));
                            }
                            catch (FormatException)
                            {
                                isPhone = false;
                            }
                        }
                    }
                    else
                        isPhone = false;

                    if (isPhone)
                    {
                        fullAddress = fullAddress.Remove(i);
                        RemoveSpaces(ref fullAddress, true, true);
                        dealer_address.SetContact(aux1);
                    }

                    // removing comments between ()
                    i = fullAddress.IndexOf('(');
                    while (i != -1)
                    {
                        int j = fullAddress.IndexOf(')', i + 1);
                        if (j != -1)
                            fullAddress = fullAddress.Replace(fullAddress.Substring(i, j - i + 1), "");
                        else
                            fullAddress = fullAddress.Replace(fullAddress.Substring(i, fullAddress.Length - i), "");
                        i = fullAddress.IndexOf('(');
                    }
                    RemoveSpaces(ref fullAddress, true, true);

                    i = fullAddress.LastIndexOf(';');
                    if (i == -1)
                        i = 0;
                    int i1 = fullAddress.LastIndexOf(',');
                    if (i1 == -1)
                        i1 = 0;
                    if (i1 > i)
                        i = i1;

                    aux1 = fullAddress.Substring(i, fullAddress.Length - i);
                    fullAddress = fullAddress.Remove(i);
                    RemoveSpaces(ref aux1, true, true);

                    i = aux1.LastIndexOf(' ');
                    if (i == -1)
                        dealer_address.SetCity(aux1);
                    else
                    {
                        aux2 = aux1.Substring(i, aux1.Length - i);
                        RemoveSpaces(ref aux2, true, true);
                        if ((aux2[1] >= '0') && (aux2[1]) <= '9')
                        {
                            dealer_address.SetPostalCode(aux2);
                            aux1 = aux1.Remove(i);
                            i = aux1.LastIndexOf(' ');
                            if (i == -1)
                            {
                                i = 0;
                            }
                            aux2 = aux1.Substring(i, aux1.Length - i);
                            RemoveSpaces(ref aux2, true, true);
                        }
                        if (aux2.Length <= 3)
                        {
                            dealer_address.SetProvince(getProvince(aux2, ref dd.data[17]));
                            aux1 = aux1.Remove(i);
                        }
                        RemoveSpaces(ref aux1, true, true);
                        dealer_address.SetCity(aux1);
                    }
                    RemoveSpaces(ref fullAddress, true, true);

                    i = fullAddress.LastIndexOf("-----");
                    if (i == -1)
                        i = 0;
                    aux1 = fullAddress.Substring(i, fullAddress.Length - i);
                    fullAddress = fullAddress.Remove(i);
                    RemoveSpaces(ref aux1, true, true);
                    dealer_address.SetStreetAddress(aux1);

                    dealer_address.SetCountry(dd.data[17]);

                    GetLatLong(dealer_address, dd);
                    dealer_address.SetMap(CreateMapLink(dd));

                    if (dd.data[19] != "")
                    { // contacts can have more than one, like phones and emails
                        aux1 = dealer_address.GetContact();
                        do
                        {
                            i = dd.data[19].LastIndexOf(";");
                            if (i == -1)
                                i = 0;
                            string aux = dd.data[19].Substring(i, dd.data[19].Length - i);
                            dd.data[19] = dd.data[19].Remove(i);
                            RemoveSpaces(ref aux, true, true);
                            if (aux1 == "")
                                aux1 = aux;
                            else if (!aux1.Contains(aux))
                            {
                                aux1 = aux1 + "; " + aux;
                            }
                        } while (dd.data[19] != "");
                        dealer_address.SetContact(aux1);
                    }

                    if ((dealer_address.GetStreetAddress() == "") && (dealer_address.GetPostalCode() == ""))
                    {
                        if (dd.data[6].ToLower() != "mobile")
                        {
                            dealer_address.SetCity("");
                            dealer_address.SetProvince("");
                            dealer_address.SetCountry("");
                        }
                        dealer_address.SetLatitude("");
                        dealer_address.SetLongitude("");
                        dealer_address.SetMap("");
                    }
                    else
                    {
                        if (dealer_address.GetStreetAddress() == "")
                            dealer_address.SetStreetAddress(dd.data[14]);
                        if (dealer_address.GetCity() == "")
                            dealer_address.SetCity(dd.data[15]);
                        if (dealer_address.GetPostalCode() == "")
                            dealer_address.SetPostalCode(dd.data[16]);
                        if (dealer_address.GetCountry() == "")
                            dealer_address.SetCountry(dd.data[17]);
                        if (dealer_address.GetProvince() == "")
                            dealer_address.SetProvince(dd.data[43]);
                    }
                    locations.SetLocation(dealer_address);
                }
            }
            locations = locations.sortByProvinceCityAndStreetAddress(locations);
            for (int loc = 0; loc < locations.CountLocations(); loc++)
            {
                location temp = locations.getLocation(loc);

                dd.data[13] = dd.data[13] != "" ? (dd.data[13] + ", ") : "";
                dd.data[13] += temp.GetStreetAddress() != "" ? (temp.GetStreetAddress() + ", ") : "";
                dd.data[13] += temp.GetCity() != "" ? (temp.GetCity() + ", ") : "";
                dd.data[13] += temp.GetProvince() != "" ? (temp.GetProvince() + ", ") : "";
                dd.data[13] += temp.GetCountry() != "" ? (temp.GetCountry() + ", ") : "";
                dd.data[13] += temp.GetPostalCode() != "" ? (temp.GetPostalCode() + ", ") : "";
                dd.data[13] += temp.GetContact() != "" ? temp.GetContact() : "";
                RemoveSpaces(ref dd.data[13], true, true);
            }
            return locations;
        }

        private bool GetAddressFromGoogleMaps(ref string address, ref location dealer_address, string dealLinkUrl, ref int timesCalled, ref int timesGoogleMaps, string province)
        {
            dealer locations = new dealer();
            string alt_address = "";
            int i = address.IndexOf(')');
            if (i != -1)
            {
                int i1 = address.IndexOf('(');
                if ((i < i1) || (i1 == -1))
                {
                    address = address.Remove(0, i + 1);
                    RemoveSpaces(ref address, true, true);
                }
            }
            if (address[0] == '#')
                address = address.Remove(0, 1);
            if (address.Contains(" Blvd"))
                address = address.Replace(" Blvd", " Boulevard");
            string temp = address.ToLower();
            i = temp.IndexOf("upper level");
            if (i != -1)
            {
                address = address.Remove(i, "upper level".Length);
                temp = temp.Remove(i, "upper level".Length);
                address = address.Replace(",,", ",");
                RemoveSpaces(ref address, true, true);
            }
            i = temp.IndexOf("lower level");
            if (i != -1)
            {
                address = address.Remove(i, "lower level".Length);
                temp = temp.Remove(i, "lower level".Length);
                address = address.Replace(",,", ",");
                RemoveSpaces(ref address, true, true);
            }
            string orig = address;
            timesCalled += 1;
            i = address.IndexOf(';');
            if (i != -1)
            {
                alt_address = address.Substring(i, address.Length - i);
                RemoveSpaces(ref alt_address, true, true);
            }
            string left_hifen = "";
            string right_hifen = "";
            string with_province = "";
            i = 0;
            do
            {
                if ((i + 1) < address.Length)
                    i = address.IndexOf('-', i + 1);
                else
                    i = -1;
                while (i == 0)
                {
                    address = address.Substring(1, address.Length - 1);
                    i = address.IndexOf('-');
                }
                if ((i != -1) && (i == address.Length - 1))
                {
                    address = address.Remove(i);
                    continue;
                }
            }
            while ((i != -1) && (((address[i - 1] >= '0') && (address[i - 1] <= '9')) && ((address[i + 1] >= '0') && (address[i + 1] <= '9'))));
            if (i != -1)
            {
                left_hifen = address.Substring(0, i);
                RemoveSpaces(ref left_hifen, true, true);
                right_hifen = address.Substring(i + 1, address.Length - (i + 1));
                RemoveSpaces(ref right_hifen, true, true);
            }
            if ((province != "") && (!address.Contains(province)))
            {
                with_province = address + ", " + province;
            }

            
            if (address != "")
            {

                while (true)
                {

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://maps.googleapis.com/maps/api/geocode/xml?address=" + address + "&sensor=false");
                    request.Accept = "Accept: text/html,application/xhtml+xml,application/xml";
                    XmlDocument strData = new XmlDocument();
                    try
                    {
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        System.IO.Stream stream = response.GetResponseStream();
                        System.Text.Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                        System.IO.StreamReader reader = new System.IO.StreamReader(stream, ec);
                        strData.LoadXml(reader.ReadToEnd());
                        reader.Close();
                        timesGoogleMaps += 1;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR: Exception reading from GoogleMapsAPI ");
                        AtTheEnd = AtTheEnd + "ERROR: Exception reading from GoogleMapsAPI. Address: " + address + " Dealsite: " + dealLinkUrl + "\n";
                        return false;
                    }

                    string st = strData.SelectSingleNode("/GeocodeResponse/status").InnerXml.ToString();
                    if (st == "OK")
                    {
                        string lat, lng;
                        string formatted_address = strData.SelectSingleNode("/GeocodeResponse/result/formatted_address").InnerXml.ToString();

                        try
                        {
                            Boolean isThis = false;
                            int count = 0;
                            location loc = new location();
                            address = orig;
                            i = formatted_address.LastIndexOf(',');
                            string country = formatted_address.Substring(i + 2, formatted_address.Length - (i + 2));
                            formatted_address = formatted_address.Remove(i);

                            i = formatted_address.LastIndexOf(',');
                            int i1 = formatted_address.IndexOf(' ', i + 2);
                            loc.SetProvince(getProvince(formatted_address.Substring(i + 2, i1 - (i + 2)), ref country));
                            loc.SetCountry(country);
                            loc.SetPostalCode(formatted_address.Substring(i1 + 1, formatted_address.Length - (i1 + 1)));
                            address = address.Replace(formatted_address.Substring(i + 2, i1 - (i + 2)), "");
//                            if (address.Contains(loc.GetPostalCode()))
//                                isThis = true;
                            address = address.Replace(loc.GetPostalCode(), "");
                            address = address.Replace(loc.GetPostalCode().Replace(" ", ""), "");
                            address = address.Replace(loc.GetCountry(), "");
                            formatted_address = formatted_address.Remove(i);

                            i = formatted_address.LastIndexOf(',');
                            if (i == -1)
                                i = -2; // will be equivalent to 0 in the next instruction
                            loc.SetCity(formatted_address.Substring(i + 2, formatted_address.Length - (i + 2)));
                            if (address.Contains(loc.GetCity()))
                                count += 1;
                            address = address.Replace(loc.GetCity(), "");
                            if (i == -2)
                                i = 0;
                            formatted_address = formatted_address.Remove(i);

                            if (formatted_address != "")
                            {
                                loc.SetStreetAddress(formatted_address);
                                if (address.Contains(formatted_address))
                                    count += 1;
                                address = address.Replace(formatted_address, "");
                            }

                            RemoveSpaces(ref address, true, true);

                            if ((count == 2) || (address == ""))
                                isThis = true;

                            lat = strData.SelectSingleNode("/GeocodeResponse/result/geometry/location/lat").InnerXml.ToString();
                            lng = strData.SelectSingleNode("/GeocodeResponse/result/geometry/location/lng").InnerXml.ToString();
                            RoundLatLong(ref lat, ref lng, ref AtTheEnd, dealLinkUrl);
                            loc.SetLatitude(lat);
                            loc.SetLongitude(lng);
                            if ((lat != "") && (lng != ""))
                                loc.SetMap("http://maps.google.com/maps?q=" + lat + ", " + lng);
                            else
                                loc.SetMap("");

                            if (isThis)
                            {
                                dealer_address = loc;
                                return true;
                            }
                            loc.SetContact(address); // just to check, later, the right address. THis data will be, then, removed 
                            locations.SetLocation(loc);
                        }
                        catch
                        {
                            st = "";
                        }
                    }
                    if (with_province != "")
                    {
                        address = with_province;
                        with_province = "";
                    }
                    if (alt_address != "")
                    {
                        address = alt_address;
                        alt_address = "";
                    }
                    else if (left_hifen != "")
                    {
                        address = left_hifen;
                        left_hifen = "";
                    }
                    else if (right_hifen != "")
                    {
                        address = right_hifen;
                        right_hifen = "";
                    }
                    else if (locations.CountLocations() == 0)
                    {
                        Console.WriteLine("WARNING: Couldn't get location's address from GoogleMapsAPI for the address: " + address + " Dealsite: " + dealLinkUrl + "\n");
                        AtTheEnd = AtTheEnd + "WARNING: Couldn't get location's address from GoogleMapsAPI for the address: " + address + " Dealsite: " + dealLinkUrl + "\n";
                        return false;
                    }
                    else
                    {
                        if (locations.CountLocations() == 1)
                        {
                            dealer_address = locations.getLocation(0);
                            dealer_address.SetContact("");
                        }
                        else
                        {
                            int smaller_size = 999999;
                            int smaller_ind = -1;
                            for (i = 0; i < locations.CountLocations(); i++)
                            {
                                dealer_address = locations.getLocation(i);
                                int size = dealer_address.GetContact().Length;
                                if (size < smaller_size)
                                {
                                    smaller_size = size;
                                    smaller_ind = i;
                                }
                            }
                            dealer_address = locations.getLocation(smaller_ind);
                            address = dealer_address.GetContact(); // removing remaining address that was stored here temporarily
                            dealer_address.SetContact("");
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private string getProvince(string province, ref string country)
        {
            if ((province == "") || (province.Length > 3))
                return province;

            string temp = country.ToLower();
            if ((temp == "usa") || (temp == "états-unis") || (temp == "stany zjednoczone") || (temp == "verenigde staten"))
               country = "United States";
            if (temp == "francja")
                country = "France";
            if ((temp == "holandia") || (temp == "Pays-Bas"))
                country = "Netherlands";
            if ((temp == "irlande") || (temp == "irlandia"))
                country = "Ireland";
            if (temp == "kanada")
                country = "Canada";
            if ((temp == "nouvelle-zélande") || (temp == "nowa zelandia"))
                country = "New Zealand";
            if ((temp == "royaume-uni") || (temp == "wielka brytania"))
                country = "United Kingdom";

            switch (province.ToUpper())
            {
                    // Australia (has some equal abrev. as Canada, New Zealand and United States)
                case "ACT":
                    return ("Australian Capital Territory");
                case "NSW":
                    return ("New South Wales");
                case "NT":
                    if (country == "Australia")
                        return ("Northern Territory");
                    else if (country == "Canada")
                        return ("Northwest Territories");
                    else return province;
                case "QLD":
                    return ("Queensland");
                case "SA":
                    return ("South Australia");
                case "TAS":
                    if (country == "Australia")
                        return ("Tasmania");
                    else if (country == "New Zealand")
                        return ("Tasman District");
                    else return province;
                case "VIC":
                    return ("Victoria");
                case "WA":
                    if (country == "Australia")
                        return ("Western Australia");
                    else if (country == "United States")
                        return ("Washington");
                    else return province;
                         
                    // Canada
                case "AB":
                    return ("Alberta");
                case "BC":
                    return ("British Columbia");
                case "MB":
                    return ("Manitoba");
                case "NB":
                    return ("New Brunswick");
                case "NL":
                    return ("Newfoundland and Labrador");
                case "NS":
                    return ("Nova Scotia");
                case "NU":
                    return ("Nunavut");
                case "ON":
                    return ("Ontario");
                case "PE":
                    return ("Prince Edward Island");
                case "QC":
                    return ("Quebec");
                case "SK":
                    return ("Saskatchewan");
                case "YT":
                    return ("Yukon");

                    // New Zealand
                case "AUK":
                    return ("Auckland");
                case "BOP":
                    return ("Bay of Plenty");
                case "CAN":
                    return ("Canterbury");
                case "HKB":
                    return ("Hawke's Bay");
                case "MWT":
                    return ("Manawatu-Wanganui");
                case "NTL":
                    return ("Northland");
                case "OTA":
                    return ("Otago");
                case "STL":
                    return ("Southland");
                case "TKI":
                    return ("Taranaki");
                case "WKO":
                    return ("Waikato");
                case "WGN":
                    return ("Wellington");
                case "WTC":
                    return ("West Coast");
                case "GIS":
                    return ("Gisborne District");
                case "MBH":
                    return ("Marlborough District");
                case "NSN":
                    return ("Nelson City");
                case "CIT":
                    return ("Chatham Islands Territory");

                    // Unites States
                case "AL":
                    return ("Alabama");
                case "AK":
                    return ("Alaska");
                case "AZ":
                    return ("Arizona");
                case "AR":
                    return ("Arkansas");
                case "CA":
                    return ("California");
                case "CO":
                    return ("Colorado");
                case "CT":
                    return ("Connecticut");
                case "DE":
                    return ("Delaware");
                case "FL":
                    return ("Florida");
                case "GA":
                    return ("Georgia");
                case "HI":
                    return ("Hawaii");
                case "ID":
                    return ("Idaho");
                case "IL":
                    return ("Illinois");
                case "IN":
                    return ("Indiana");
                case "IA":
                    return ("Iowa");
                case "KS":
                    return ("Kansas");
                case "KY":
                    return ("Kentucky");
                case "LA":
                    return ("Louisiana");
                case "ME":
                    return ("Maine");
                case "MD":
                    return ("Maryland");
                case "MA":
                    return ("Massachusetts");
                case "MI":
                    return ("Michigan");
                case "MN":
                    return ("Minnesota");
                case "MS":
                    return ("Mississipi");
                case "MO":
                    return ("Missouri");
                case "MT":
                    return ("Montana");
                case "NE":
                    return ("Nebraska");
                case "NV":
                    return ("Nevada");
                case "NH":
                    return ("New Hampshire");
                case "NJ":
                    return ("New Jersey");
                case "NM":
                    return ("New Mexico");
                case "NY":
                    return ("New York");
                case "NC":
                    return ("North Carolina");
                case "ND":
                    return ("North Dakota");
                case "OH":
                    return ("Ohio");
                case "OK":
                    return ("Oklahoma");
                case "OR":
                    return ("Oregon");
                case "PA":
                    return ("Pennsylvania");
                case "RI":
                    return ("Rhode Island");
                case "SC":
                    return ("South Carolina");
                case "SD":
                    return ("South Dakota");
                case "TN":
                    return ("Tennessee");
                case "TX":
                    return ("Texas");
                case "UT":
                    return ("Utah");
                case "VT":
                    return ("Vermont");
                case "VA":
                    return ("Virginia");
                case "WV":
                    return ("West Virginia");
                case "WI":
                    return ("Wisconsin");
                case "WY":
                    return ("Wyoming");
                case "DC":
                    return ("District of Columbia");
                case "AS":
                    return ("American Samoa");
                case "GU":
                    return ("Guam");
                case "MP":
                    return ("Northern Mariana Islands");
                case "PR":
                    return ("Puerto Rico");
                case "UM":
                    return (" United States Minor Outlying Islands");
                case "VI":
                    return ("Virgin Islands, U.S.");
                case "":
                    return ("");
                default:
                    return province;
            }
        }

        private void GetLatLong(location dealer_address, Tags dd)
            // ??? Get latitude and Longitude from Googlemaps, based on the current address, and store them into DealerAddress fields AND dd ??????????????????????????????
        {
            string comma = "";
            string address, alt_address = "";
            if (dealer_address.GetStreetAddress() != "")
                comma = ", ";
            address= dealer_address.GetStreetAddress();

            address = address.Replace("#", "-");
            if (address.Contains(" Blvd"))
                address = address.Replace(" Blvd", " Boulevard");
            string temp = address.ToLower();
            int i = temp.IndexOf("upper level");
            if (i != -1)
            {
                address = address.Remove(i, "upper level".Length);
                temp = temp.Remove(i, "upper level".Length);
                address = address.Replace(",,", ",");
                RemoveSpaces(ref address, true, true);
            }
            i = temp.IndexOf("lower level");
            if (i != -1)
            {
                address = address.Remove(i, "lower level".Length);
                temp = temp.Remove(i, "lower level".Length);
                address = address.Replace(",,", ",");
                RemoveSpaces(ref address, true, true);
            }

            i = address.IndexOf(';');
            if (i != -1)
            {
                alt_address = address.Substring(i, address.Length - i);
                RemoveSpaces(ref alt_address, true, true);
            }

            if (dealer_address.GetCity() != "")
            {
                address = address + comma + dealer_address.GetCity();
                comma = ", ";
            }
            if (dealer_address.GetProvince() != "")
            {
                address = address + comma + dealer_address.GetProvince();
                comma = ", ";
            }
            if (dealer_address.GetCountry() != "")
            {
                address = address == "" ? dealer_address.GetCountry() : address + ", " + dealer_address.GetCountry();
                comma = ", ";
            }
            if (dealer_address.GetPostalCode() != "")
            {
                address = address + comma + dealer_address.GetPostalCode();
            }

            dd.data[11] = "";
            dd.data[12] = "";

            if (address != "")
            {
                for (i = 0; i < 2; i++)
                {
                    if (address[0] == '#')
                        address = address.Remove(0, 1);
                    if (address.Contains(" Blvd"))
                        address = address.Replace(" Blvd", " Boulevard");

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://maps.googleapis.com/maps/api/geocode/xml?address=" + address + "&sensor=false");
                    request.Accept = "Accept: text/html,application/xhtml+xml,application/xml";
                    XmlDocument strData = new XmlDocument();
                    try
                    {
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        System.IO.Stream stream = response.GetResponseStream();
                        System.Text.Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                        System.IO.StreamReader reader = new System.IO.StreamReader(stream, ec);
                        strData.LoadXml(reader.ReadToEnd());
                        reader.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("ERROR: Exception reading from GoogleMapsAPI ");
                        AtTheEnd = AtTheEnd + "ERROR: Exception reading from GoogleMapsAPI. Address: " + address + " Dealsite: " + dd.data[5] + "\n";
                        return;
                    }

                    string st = strData.SelectSingleNode("/GeocodeResponse/status").InnerXml.ToString();
                    if (st == "OK")
                    {
                        dd.data[11] = strData.SelectSingleNode("/GeocodeResponse/result/geometry/location/lat").InnerXml.ToString();
                        dd.data[12] = strData.SelectSingleNode("/GeocodeResponse/result/geometry/location/lng").InnerXml.ToString();
                        RoundLatLong(ref dd.data[11], ref dd.data[12], ref AtTheEnd, dd.data[5]);
                        break;
                    }
                    else
                    {
                        if (alt_address != "")
                        {
                            address = address.Replace(dealer_address.GetStreetAddress(), alt_address);
                            alt_address = "";
                        }
                        else
                        {
                            Console.WriteLine("WARNING: Couldn't get Lat/Lng from GoogleMapsAPI for the address: " + address + " Dealsite: " + dd.data[5] + "\n");
                            AtTheEnd = AtTheEnd + "WARNING: Couldn't get Lat/Lng from GoogleMapsAPI for the address: " + address + " Dealsite: " + dd.data[5] + "\n";
                        }
                    }
                }
            }
            // apenas para nao ficar vazio
            dealer_address.SetLongitude(dd.data[11]);
            dealer_address.SetLatitude(dd.data[12]);
        }

        private string CreateMapLink(Tags dd)
        // Creating/changing the map link
        {
            // based on Latitude and Longitude
            if ((dd.data[11] != "") && (dd.data[12] != ""))
            {
                return("http://maps.google.com/maps?q=" + dd.data[11] + ", " + dd.data[12]);
            }
            else
                return("");
  //          return dd.data[18];
        }

        private void DealersPreProcessingData(Tags dd, List<string> removeText)
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

            if ((dd.data[17] != "") && ((dd.data[17][0] >= 'a') && (dd.data[17][0] <= 'z')))
                dd.data[17] = dd.data[17].ToUpper().Substring(0, 1) + dd.data[17].Substring(1, dd.data[17].Length - 1);

           
            Boolean phone = false;
            for (int i = 13; i <= 43; i++)
            {
                if (i == 16)
                    i = 43;
                dd.data[i] = RemoveWebLinks(dd.data[i]);
                string aux = dd.data[i].ToLower();
                string aux1 = dd.data[i];
                int first;
//                string aux1 = aux;
                if (aux1 != "")
                {
                    if (i != 13)
                        aux1 = ExtractPhone(aux1, dd, ref phone);

                    aux = aux.Replace("call ", " ");
                    aux1 = aux1.Replace("call ", " ");
                    aux1 = aux1.Replace("Call ", " ");
                    aux = aux.Replace("call\n", "\n");
                    aux1 = aux1.Replace("call\n", "\n");
                    aux1 = aux1.Replace("Call\n", "\n");

                    for (int q = 0; q < removeText.Count; q++)
                    {
                        do
                        {
                            string text = removeText[q];
                            first = aux.IndexOf(text);
                            if (first != -1)
                            {
                                aux = aux.Remove(first, text.Length);
                                aux1 = aux1.Remove(first, text.Length);
                                first = aux.IndexOf(text);
                            }
                        } while (first != -1);
                    }

                    first = aux.IndexOf("mobile");
                    while (first != -1)
                    {
                        // there is an american city called Mobile.
                        if (aux1.Substring(first, 7) != "Mobile,")
                        {
                            aux = aux.Remove(first, 6);
                            aux1 = aux1.Remove(first, 6);
                        }
                        if (first < aux.Length)
                            first = aux.IndexOf("mobile", first + 1);
                        else
                            first = -1;
                    }
/*                    aux = aux.Replace("include photo", "");
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
                    aux = aux.Replace("mail out", "");*/
                    if (aux1 != dd.data[i])
                    {
                        if (i == 13)
                            RemoveSpaces(ref aux1, true, false);
                        else
                            RemoveSpaces(ref aux1, true, true);
                        if (aux1 == "or") aux = "";
                        dd.data[i] = aux1;
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
                    RemoveSpaces(ref dd.data[15], true, true);
                    RemoveSpaces(ref dd.data[43], true, true);
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

            double lat = -1, longit = -1;
            if (dd.data[11] != "")
            {
                try
                {
                    lat = Convert.ToDouble(dd.data[11]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '" + dd.data[11] + "' to a Double (Latitude). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert '" + dd.data[11] + "' to a Double (Latitude). Dealsite: " + dd.data[5] + "\n";
                    dd.data[11] = "";
                    lat = 0;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'" + dd.data[11] + "' is outside the range of a Double (Latitude). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "'" + dd.data[11] + "' is outside the range of a Double (Latitude). Dealsite: " + dd.data[5] + "\n";
                    dd.data[11] = "";
                    lat = 0;
                }
            }
            if (dd.data[12] != "")
            {
                try
                {
                    longit = Convert.ToDouble(dd.data[12]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '" + dd.data[12] + "' to a Double (Longitude). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert '" + dd.data[12] + "' to a Double (Longitude). Dealsite: " + dd.data[5] + "\n";
                    dd.data[12] = "";
                    longit = 0;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'" + dd.data[12] + "' is outside the range of a Double (Longitude). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "'" + dd.data[12] + " is outside the range of a Double (Longitude). Dealsite: " + dd.data[5] + "\n";
                    dd.data[12] = "";
                    longit = 0;
                }
            }
            if ((lat == 0) && (longit == 0))
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

   //         RoundLatLong(ref dd.data[11], ref dd.data[12], ref AtTheEnd);

        }

        private void VouchersHandling(Tags dd)
        {
            dd.data[35] = dd.data[35].ToLower();
            dd.data[35] = dd.data[35].Replace("sold out", "");
            dd.data[35] = dd.data[35].Replace("buys", "");
            dd.data[35] = dd.data[35].Replace("buy", "");
            dd.data[35] = dd.data[35].Replace("bought", "");
            dd.data[35] = dd.data[35].Replace("over", "");
            dd.data[35] = dd.data[35].Replace("achats", "");
            dd.data[35] = dd.data[35].Replace("achat", "");
            dd.data[35] = dd.data[35].Replace(",", "");
            RemoveSpaces(ref dd.data[35], true, true);

            dd.data[31] = dd.data[31].ToLower();
            dd.data[31] = dd.data[31].Replace("sold out", "");
            dd.data[31] = dd.data[31].Replace("buys", "");
            dd.data[31] = dd.data[31].Replace("buy", "");
            dd.data[31] = dd.data[31].Replace("bought", "");
            dd.data[31] = dd.data[31].Replace("over", "");
            dd.data[31] = dd.data[31].Replace("achats", "");
            dd.data[31] = dd.data[31].Replace("achat", "");
            dd.data[31] = dd.data[31].Replace(",", "");
            dd.data[31] = dd.data[31].Replace("one", "1");
            RemoveSpaces(ref dd.data[31], true, true);

            if (dd.data[30] == "0")
                dd.data[30] = "";

            int i = dd.data[31].IndexOf('+');
            if ((i != -1) && (i + 1 < dd.data[31].Length))
            {
                int n1, n2 = 0;
                try
                {
                    n1 = Convert.ToInt32(dd.data[31].Substring(0, i));
                    string temp = dd.data[31].Substring(i + 1, dd.data[31].Length - (i + 1));
                    i = temp.IndexOf(";&;");
                    if (i != -1)
                    {
                        while (temp != "")
                        {
                            n2 += Convert.ToInt32(temp.Substring(0, i));
                            if (i < temp.Length)
                                temp = temp.Remove(0, i + 3);
                            else
                                temp = "";
                            i = temp.IndexOf(";&;");
                            if (i == -1)
                                i = temp.Length;
                        }
                    }
                    else
                        n2 = Convert.ToInt32(temp);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to extract two Int numbers (MinNumberVouchers) from '" + dd.data[31] + ". Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to extract two Int numbers (MinNumberVouchers) from '" + dd.data[31] + ".: " + dd.data[5] + "\n";
                    n1 = -1;
                    n2 = -1;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("One of those numbers'" + dd.data[31] + "' is outside the range of an Int (MinNumberVouchers).: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "One of those numbers'" + dd.data[31] + " is outside the range of an Int (MinNumberVouchers).: " + dd.data[5] + "\n";
                    n1 = -1;
                    n2 = -1;
                }
                dd.data[31] = n1 == -1 ? "" : (n1 + n2).ToString();
            }

            if (dd.data[31] != "")
            {
                try
                {
                    Convert.ToInt32(dd.data[31]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert to Int (MinNumberVouchers) from '" + dd.data[31] + ". Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert to Int (MinNumberVouchers) from '" + dd.data[31] + ".: " + dd.data[5] + "\n";
                    dd.data[31] = "";
                }
                catch (OverflowException)
                {
                    Console.WriteLine("One of those numbers'" + dd.data[31] + "' is outside the range of an Int (MinNumberVouchers).: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "One of those numbers'" + dd.data[31] + " is outside the range of an Int (MinNumberVouchers).: " + dd.data[5] + "\n";
                    dd.data[31] = "";
                }
            }

            if (dd.data[30] != "")
            {
                try
                {
                    Convert.ToInt32(dd.data[30]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert to Int (MaxNumberVouchers) from '" + dd.data[30] + ". Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert to Int (MaxNumberVouchers) from '" + dd.data[30] + ".: " + dd.data[5] + "\n";
                    dd.data[30] = "";
                }
                catch (OverflowException)
                {
                    Console.WriteLine("One of those numbers'" + dd.data[30] + "' is outside the range of an Int (MaxNumberVouchers).: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "One of those numbers'" + dd.data[30] + " is outside the range of an Int (MaxNumberVouchers).: " + dd.data[5] + "\n";
                    dd.data[30] = "";
                }
            }

            if (dd.data[35] != "")
            {
                try
                {
                    Convert.ToInt32(dd.data[35]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert to Int (PaidVoucherCount) from '" + dd.data[35] + ". Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert to Int (PaidVoucherCount) from '" + dd.data[35] + ".: " + dd.data[5] + "\n";
                    dd.data[35] = "";
                }
                catch (OverflowException)
                {
                    Console.WriteLine("One of those numbers'" + dd.data[35] + "' is outside the range of an Int (PaidVoucherCount).: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "One of those numbers'" + dd.data[35] + " is outside the range of an Int (PaidVoucherCount).: " + dd.data[5] + "\n";
                    dd.data[35] = "";
                }
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
            int format = 0;
            try
            {
                format = Convert.ToInt16(dd.data[29].Substring(1, i - 1));
            }
            catch (FormatException)
            {
                Console.WriteLine("ExpiryTime DESCRIPTION format is not a number.'" + dd.data[29].Substring(1, i - 1) + " Dealsite: " + dd.data[49] + "\n");
                AtTheEnd = AtTheEnd + "ExpiryTime DESCRIPTION format is not a number.'" + dd.data[29].Substring(1, i - 1) + " Dealsite: " + dd.data[49] + "\n";
                dd.data[28] = "";
                dd.data[29] = "";
                return;
            }
            catch (OverflowException)
            {
                Console.WriteLine("One of those format numbers'" + dd.data[31] + "' is outside the range of a Double (ExpiryTime).: " + " Dealsite: " + dd.data[49] + "\n");
                AtTheEnd = AtTheEnd + "One of those format numbers'" + dd.data[31] + " is outside the range of a Double (ExpiryTime).: " + " Dealsite: " + dd.data[49] + "\n";
                dd.data[28] = "";
                dd.data[29] = "";
                return;
            }

            dd.data[29] = dd.data[29].Remove(0, i + 1);

            if (format == 1) // represents time by seconds (elapsed and total)
            {
                double aux;
                try
                {
                    aux = Convert.ToDouble(dd.data[26]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert " + dd.data[26] + " to Double (SecondsTotal). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert " + dd.data[26] + " to Double (SecondsTotal). Dealsite: " + dd.data[5] + "\n";
                    aux = -1;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'" + dd.data[26] + "' is outside the range of a Double (SecondsTotal). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "'" + dd.data[26] + " is outside the range of a Double (SecondsTotal). Dealsite: " + dd.data[5] + "\n";
                    aux = -1;
                }
                if (aux == -1)
                {
                    dd.data[26] = "";
                    dd.data[27] = "";
                    dd.data[28] = "";
                    dd.data[29] = "";
                    return;
                }
                TimeSpan total = TimeSpan.FromSeconds(aux);

                try
                {
                    aux = Convert.ToDouble(dd.data[27]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert " + dd.data[27] + " to Double (SecondsElapsed). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert " + dd.data[27] + " to Double (SecondsElapsed). Dealsite: " + dd.data[5] + "\n";
                    aux = -1;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'" + dd.data[27] + "' is outside the range of a Double (SecondsElapsed). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "'" + dd.data[27] + " is outside the range of a Double (SecondsElapsed). Dealsite: " + dd.data[5] + "\n";
                    aux = -1;
                }
                if (aux == -1)
                {
                    dd.data[27] = "";
                    dd.data[28] = "";
                    dd.data[29] = "";
                    return;
                }
                TimeSpan elapsed = TimeSpan.FromSeconds(aux);

                DateTimeOffset extracted = DateTimeOffset.Parse(dd.data[1]);
                dd.data[28] = (total - elapsed).ToString();
                dd.data[29] = (extracted.Add(total - elapsed)).ToString();
                return;
            }
            if (format == 2)
            {
                int hr, min, sec, day;
                string remaining = dd.data[28];
                dd.data[26] = "";
                dd.data[27] = "";
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
                        try
                        {
                            day = Convert.ToInt32(remaining.Substring(0, i));
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Unable to convert " + remaining + " to Int (Day - RemainingTime). Dealsite: " + dd.data[5] + "\n");
                            AtTheEnd = AtTheEnd + "Unable to convert " + remaining + " to Int (Day - RemainingTime). Dealsite: " + dd.data[5] + "\n";
                            day = -1;
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("'" + remaining + "' is outside the range of a Int (Day - RemainingTime). Dealsite: " + dd.data[5] + "\n");
                            AtTheEnd = AtTheEnd + "'" + remaining + " is outside the range of a Int (Day - RemainingTime). Dealsite: " + dd.data[5] + "\n";
                            day = -1;
                        }
                        if (day == -1)
                        {
                            dd.data[28] = "";
                            dd.data[29] = "";
                            return;
                        }
                    }
                    remaining = remaining!=""?remaining.Remove(0, i + 1):"";
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
                        {
                            try
                            {
                                sec = Convert.ToInt32(remaining.Substring(i + 1, remaining.Length - i - 1));
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Unable to convert " + remaining + " to Int (Seconds - RemainingTime). Dealsite: " + dd.data[5] + "\n");
                                AtTheEnd = AtTheEnd + "Unable to convert " + remaining + " to Int (Seconds - RemainingTime). Dealsite: " + dd.data[5] + "\n";
                                sec = -1;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("'" + remaining + "' is outside the range of a Int (Seconds - RemainingTime). Dealsite: " + dd.data[5] + "\n");
                                AtTheEnd = AtTheEnd + "'" + remaining + " is outside the range of a Int (Seconds - RemainingTime). Dealsite: " + dd.data[5] + "\n";
                                sec = -1;
                            }
                            if (sec == -1)
                            {
                                dd.data[28] = "";
                                dd.data[29] = "";
                                return;
                            }
                        }
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
                        try
                        {
                            day = Convert.ToInt32(remaining);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Unable to convert " + remaining + " to Int (Day - RemainingTime). Dealsite: " + dd.data[5] + "\n");
                            AtTheEnd = AtTheEnd + "Unable to convert " + remaining + " to Int (Day - RemainingTime). Dealsite: " + dd.data[5] + "\n";
                            day = -1;
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("'" + remaining + "' is outside the range of a Int (Day - RemainingTime). Dealsite: " + dd.data[5] + "\n");
                            AtTheEnd = AtTheEnd + "'" + remaining + " is outside the range of a Int (Day - RemainingTime). Dealsite: " + dd.data[5] + "\n";
                            day = -1;
                        }
                        if (sec == -1)
                        {
                            dd.data[28] = "";
                            dd.data[29] = "";
                            return;
                        }
                        remaining = "";
                    }
                }
                i = remaining.LastIndexOf(':');
                if (i != -1)
                {
                    if ((i + 1) < remaining.Length)
                    {
                        if (remaining[i + 1] != '-')
                        {
                            try
                            {
                                min = Convert.ToInt32(remaining.Substring(i + 1, remaining.Length - i - 1));
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Unable to convert " + remaining + " to Int (Minutes - RemainingTime). Dealsite: " + dd.data[5] + "\n");
                                AtTheEnd = AtTheEnd + "Unable to convert " + remaining + " to Int (Minutes - RemainingTime). Dealsite: " + dd.data[5] + "\n";
                                min = -1;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("'" + remaining + "' is outside the range of a Int (Minutes - RemainingTime). Dealsite: " + dd.data[5] + "\n");
                                AtTheEnd = AtTheEnd + "'" + remaining + " is outside the range of a Int (Minutes - RemainingTime). Dealsite: " + dd.data[5] + "\n";
                                min = -1;
                            }
                            if (min == -1)
                            {
                                dd.data[28] = "";
                                dd.data[29] = "";
                                return;
                            }
                        }
                        remaining = remaining.Remove(i, remaining.Length - i);
                    }
                    else
                        remaining = remaining.Remove(i, 1);
                }
                remaining = remaining.Replace("-","");
                if (remaining.Length > 0)
                {
                    try
                    {
                        hr = Convert.ToInt32(remaining);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Unable to convert " + remaining + " to Int (Hours - RemainingTime). Dealsite: " + dd.data[5] + "\n");
                        AtTheEnd = AtTheEnd + "Unable to convert " + remaining + " to Int (Hours - RemainingTime). Dealsite: " + dd.data[5] + "\n";
                        hr = -1;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("'" + remaining + "' is outside the range of a Int (Hours - RemainingTime). Dealsite: " + dd.data[5] + "\n");
                        AtTheEnd = AtTheEnd + "'" + remaining + " is outside the range of a Int (Hours - RemainingTime). Dealsite: " + dd.data[5] + "\n";
                        hr = -1;
                    }
                    if (hr == -1)
                    {
                        dd.data[28] = "";
                        dd.data[29] = "";
                        return;
                    }
                }
                time = new TimeSpan(day, hr, min, sec);
                dd.data[28] = time.ToString();
                dd.data[29] = extracted.Add(time).ToString();
                return;
            }
            if (format == 3)
            {
                DateTimeOffset Exp, Rem;
                dd.data[26] = "";
                dd.data[27] = "";
                dd.data[29] = dd.data[29].ToUpper();
                dd.data[29] = dd.data[29].Replace("GMT","");

                try
                {
                    Exp = DateTimeOffset.Parse(dd.data[29]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert " + dd.data[29] + " to DateTimeOffset (ExpiryTime). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert " + dd.data[29] + " to DateTimeOffset (ExpiryTime). Dealsite: " + dd.data[5] + "\n";
                    dd.data[28] = "";
                    dd.data[29] = "";
                    return;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("(ExpiryTime) is null. Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "(ExpiryTime) is null. Dealsite: " + dd.data[5] + "\n";
                    dd.data[28] = "";
                    dd.data[29] = "";
                    return;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("'" + dd.data[29] + "' offset is greater than 14 hours (ExpiryTime). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "'" + dd.data[29] + "' offset is greater than 14 hours (ExpiryTime). Dealsite: " + dd.data[5] + "\n";
                    dd.data[28] = "";
                    dd.data[29] = "";
                    return;
                }

                Rem = DateTimeOffset.Parse(dd.data[1]);
                dd.data[28] = (Exp - Rem).ToString();
                return;
            }
            if (format == 4)
            {
                double aux;
                if (dd.data[26] == "")
                {
                    if (dd.data[34] == "true")
                    {
                        Console.WriteLine("Missing (SecondsTotal) data. Dealsite: " + dd.data[5] + "\n");
                        AtTheEnd = AtTheEnd + "Missing (SecondsTotal) data. Dealsite: " + dd.data[5] + "\n";
                    }
                    aux = -1;
                }
                else
                {
                    try
                    {
                        aux = Convert.ToDouble(dd.data[26]);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Unable to convert " + dd.data[26] + " to Double (SecondsTotal). Dealsite: " + dd.data[5] + "\n");
                        AtTheEnd = AtTheEnd + "Unable to convert " + dd.data[26] + " to Double (SecondsTotal). Dealsite: " + dd.data[5] + "\n";
                        aux = -1;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("'" + dd.data[26] + "' is outside the range of a Double (SecondsTotal). Dealsite: " + dd.data[5] + "\n");
                        AtTheEnd = AtTheEnd + "'" + dd.data[26] + " is outside the range of a Double (SecondsTotal). Dealsite: " + dd.data[5] + "\n";
                        aux = -1;
                    }
                }
                if (aux == -1)
                {
                    dd.data[26] = "";
                    dd.data[27] = "";
                    dd.data[28] = "";
                    dd.data[29] = "";
                    return;
                }

                DateTimeOffset old = DateTimeOffset.Parse("1/1/1970 0:0:0 AM -00:00");
                DateTimeOffset deadline = old.AddSeconds(aux);
                deadline = deadline.ToLocalTime();
                DateTimeOffset extracted = DateTimeOffset.Parse(dd.data[1]);
                TimeSpan dif1 = extracted.Subtract(old);
                dd.data[28] = deadline.Subtract(extracted).ToString();
                dd.data[29] = deadline.ToString();
                return;
            }

            if (format == 5)
            {
                string month, day, year, remainder;
                DateTimeOffset current;
                remainder = " 12:00 AM -0500";
                int space = dd.data[29].IndexOf(" ");
                month = dd.data[29].Substring(0, space);
                RemoveSpaces(ref month, true, true); ;
                day = dd.data[29].Substring(space, dd.data[29].Length - space);
                RemoveSpaces(ref day, true, true);

                switch (month.ToLower())
                {
                    case "january":
                        month = "01";
                        break;
                    case "february":
                        month = "02";
                        break;
                    case "march":
                        month = "03";
                        break;
                    case "april":
                        month = "04";
                        break;
                    case "may":
                        month = "05";
                        break;
                    case "june":
                        month = "06";
                        break;
                    case "july":
                        month = "07";
                        break;
                    case "august":
                        month = "08";
                        break;
                    case "september":
                        month = "09";
                        break;
                    case "october":
                        month = "10";
                        break;
                    case "november":
                        month = "11";
                        break;
                    case "december":
                        month = "12";
                        break;
                    default:
                        dd.data[29] = "";
                        dd.data[28] = "";
                        break;
                }

                current = DateTimeOffset.Parse(dd.data[1]);
                year = current.Year.ToString();
                if (current.Month > Convert.ToInt16(month))
                {
                    year = (Convert.ToInt16(year) + 1).ToString();
                }

                dd.data[29] = month + "/" + day + "/" + year + remainder;
                dd.data[28] = (DateTimeOffset.Parse(dd.data[29]) - current).ToString();
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

            string temp = dd.data[21].ToLower();
            if (temp.Contains("sold out"))
            {
                dd.data[32] = "true";
                dd.data[21] = "";
            }

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
            {
                try
                {
                    regular = Convert.ToDouble(dd.data[20]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '" + dd.data[20] + "' to Double (Regular Price). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert '" + dd.data[20] + "' to Double (Regular Price). Dealsite: " + dd.data[5] + "\n";
                    regular = -1;
                    dd.data[20] = "";
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'" + dd.data[20] + "' is outside the range of a Double (Regular Price). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "'" + dd.data[20] + "' is outside the range of a Double (Regular Price). Dealsite: " + dd.data[5] + "\n";
                    regular = -1;
                    dd.data[20] = "";
                }
            }
            else
                regular = -1;

            if (dd.data[21] != "")
            {
                try
                {
                    our = Convert.ToDouble(dd.data[21]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '" + dd.data[21] + "' to Double (Our Price). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert '" + dd.data[21] + "' to Double (Our Price). Dealsite: " + dd.data[5] + "\n";
                    our = -1;
                    dd.data[21] = "";
                    dd.data[20] = "";
                    dd.data[22] = "";
                    dd.data[23] = "";
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'" + dd.data[21] + "' is outside the range of a Double (Our Price). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "'" + dd.data[21] + "' is outside the range of a Double (Our Price). Dealsite: " + dd.data[5] + "\n";
                    our = -1;
                    dd.data[21] = "";
                }
            }
            else
                our = -1;

            if (dd.data[22] != "")
            {
                try
                {
                    save = Convert.ToDouble(dd.data[22]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '" + dd.data[22] + "' to Double (Save). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert '" + dd.data[22] + "' to Double (Save). Dealsite: " + dd.data[5] + "\n";
                    save = -1;
                    dd.data[22] = "";
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'" + dd.data[22] + "' is outside the range of a Double (Save). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "'" + dd.data[22] + "' is outside the range of a Double (Save). Dealsite: " + dd.data[5] + "\n";
                    save = -1;
                    dd.data[22] = "";
                }
            }
            else
                save = -1;

            if (dd.data[23] != "")
            {
                dd.data[23] = dd.data[23].Replace("%", "");
                try
                {
                    disc = Convert.ToDouble(dd.data[23]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '" + dd.data[23] + "' to Double (Discount). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert '" + dd.data[23] + "' to Double (Discount). Dealsite: " + dd.data[5] + "\n";
                    disc = -1;
                    dd.data[23] = "";
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'" + dd.data[23] + "' is outside the range of a Double (Discount). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "'" + dd.data[23] + "' is outside the range of a Double (Discount). Dealsite: " + dd.data[5] + "\n";
                    disc = -1;
                    dd.data[23] = "";
                }
            }
            else
                disc = -1;

            if ((regular == -1) && ((our != -1) && (disc != -1)))
            {
                if (our != 0)
                    regular = Math.Round(((our / (100 - disc)) * 100), 2);
                dd.data[20] = regular.ToString();
            }

            if ((save == -1) && ((regular != -1) && (our != -1)))
            {
                save = regular - our;
                dd.data[22] = save.ToString();
            }

            if (dd.data[24] != "")
            {
                try
                {
                    Convert.ToDouble(dd.data[24]);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '" + dd.data[24] + "' to Double (PayOutAmount). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert '" + dd.data[24] + "' to Double (PayOutAmount). Dealsite: " + dd.data[5] + "\n";
                    dd.data[24] = "";
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'" + dd.data[24] + "' is outside the range of a Double (PayOutAmount). Dealsite: " + dd.data[5] + "\n");
                    AtTheEnd = AtTheEnd + "'" + dd.data[24] + "' is outside the range of a Double (PayOutAmount). Dealsite: " + dd.data[5] + "\n";
                    dd.data[24] = "";
                }
            }
        }

        private void RoundLatLong(ref string p, ref string p_2, ref string AtTheEnd, string site)
        {
            // Round Latitude to 4 decimals
            if (p != "")
            {
                double lat = 0;
                
                try
                {
                    lat = Convert.ToDouble(p);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '" + p + "' to a Double (Latitude). Dealsite: " + site + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert '" + p + "' to a Double (Latitude). Dealsite: " + site + "\n";
                    lat = 0;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'" + p + "' is outside the range of a Double (Latitude). Dealsite: " + site + "\n");
                    AtTheEnd = AtTheEnd + "'" + p + "' is outside the range of a Double (Latitude). Dealsite: " + site + "\n";
                    lat = 0;
                }
                lat = Math.Round(lat, 4);
                p = lat == 0 ? "" : lat.ToString();
            }

            // Round Longitude to 4 decimals
            if (p_2 != "")
            {
                double longit = 0;
                try
                {
                    longit = Convert.ToDouble(p_2);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Unable to convert '" + p_2 + "' to a Double (Longitude). Dealsite: " + site + "\n");
                    AtTheEnd = AtTheEnd + "Unable to convert '" + p_2 + "' to a Double (Longitude). Dealsite: " + site + "\n";
                    longit = 0;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("'" + p_2 + "' is outside the range of a Double. Dealsite: " + site + "\n");
                    AtTheEnd = AtTheEnd + "'" + p_2 + "' is outside the range of a Double. Dealsite: " + site + "\n";
                    longit = 0;
                }
                longit = Math.Round(longit, 4);
                p_2 = longit == 0 ? "" : longit.ToString();
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
                int e1 = aux.IndexOf(';', b);
                if ((e == -1) || ((e1 != -1) && (e1 < e)))
                    e = e1;
                if (e == -1)
                    e = aux.Length - 1;
                int pos_dot = aux.IndexOf('.', b);
                if (pos_dot == -1)
                    pos_dot = aux.Length;
                if ((pos_dot < e) && (pos_dot - b < 30) && (e - pos_dot < 30))
                { // it can be an email address only if there is a dot (.) between '@' and the end of string ('e')
                    int b1 = aux.LastIndexOf(" ", b);
                    if (b1 == -1)
                        b1 = 0;
                    int b2 = aux.LastIndexOf(';', b);
                    if (b2 == -1)
                        b2 = 0;
                    if (b2 > b1)
                        b1 = b2;
                    b2 = aux.LastIndexOf('#', b);
                    if (b2 == -1)
                        b2 = 0;
                    else
                        b2 += 1;
                    b = b2 > b1 ? b2 : b1;
                    temp = aux.Substring(b, e - b + 1);
                    RemoveSpaces(ref temp, true, true);
                    if (temp.IndexOf("(") == -1)
                    {
                        if (contact.IndexOf(temp) == -1)
                        {
                            if (contact == "")
                                contact = temp + ";";
                            else
                                contact = temp + "; " + contact;
                        }
                        e = 0;
                    }
                    aux = aux.Replace(temp, "");
                    RemoveSpaces(ref aux, true, false);
                }
                b = aux.IndexOf("@", e);
            }
        }

        private string RemoveWebLinks(string aux)
        {
            int b = aux.IndexOf("http://");
            while (b != -1)
            {
                int e = aux.IndexOf(" ", b);
                int e1 = aux.IndexOf(';', b);
                if ((e == -1) || ((e1 != -1) && (e1 < e)))
                    e = e1;
                if (e != -1)
                    aux = aux.Replace(aux.Substring(b, e - b + 1),"");
                else
                    aux = aux.Replace(aux.Substring(b, aux.Length - b),"");
                RemoveSpaces(ref aux, true, false);
                b = aux.IndexOf("http://");
            }
            b = aux.IndexOf("www.");
            while (b != -1)
            {
                int e = aux.IndexOf(" ", b);
                int e1 = aux.IndexOf(';', b);
                if ((e == -1) || ((e1 != -1) && (e1 < e)))
                    e = e1;
                if (e != -1)
                    aux = aux.Replace(aux.Substring(b, e - b + 1),"");
                else
                    aux = aux.Replace(aux.Substring(b, aux.Length - b),"");
                RemoveSpaces(ref aux, true, false);
                b = aux.IndexOf("www.");
            }
            b = aux.IndexOf(".com");
            if (b == -1)
                b = aux.IndexOf(".ca");
            while (b != -1)
            {
                int e = b;

                int b1 = aux.LastIndexOf(" ", b);
                if (b1 == -1)
                    b1 = 0;
                int b2 = aux.LastIndexOf(';', b);
                if (b2 == -1)
                    b2 = 0;
                if (b2 > b1)
                    b1 = b2;
                b2 = aux.LastIndexOf('#', b);
                if (b2 == -1)
                    b2 = 0;
                else
                    b2 += 1;
                b = b2 > b1 ? b2 : b1;

                e = aux.IndexOf(" ", e);
                int e1 = aux.IndexOf(';', b);
                if ((e == -1) || ((e1 != -1) && (e1 < e)))
                    e = e1;
                if (e == -1)
                    e = aux.Length - 1;
                int a = aux.IndexOf("@", b);
                if ((a == -1) || (a >= e)) // it is not an email address
                {
                    aux = aux.Replace(aux.Substring(b, e - b + 1), "");
                    RemoveSpaces(ref aux, true, false);
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

        private string ExtractPhone(string original, Tags dd, ref Boolean phone) // phone is only true if previous column indicates that the current one probably has a phone number
        {
            int b;
            string aux = original.ToLower();
            if (phone == false)
            {
                original = original.Replace(" at ", " ");
                aux = aux.Replace(" at ", " ");
                b = aux.IndexOf("phone");
                if (b == -1)
                    b = aux.IndexOf("call");
                if (b != -1)
                {
                    if ((b - 1 >= 0) && (((aux[b - 1] >= 'a') && (aux[b - 1] <= 'z')) || ((aux[b - 1] >= 'A') && (aux[b - 1] <= 'Z'))))
                        return original;
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
 //                   string phoneNumberAux = "";
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
                    {
                        phoneNumber = original.Substring(b, original.Length - b);
 //                       phoneNumberAux = aux.Substring(b, aux.Length - b);
                    }
                    else
                    {
                        phoneNumber = original.Substring(b, e - b);
  //                      phoneNumberAux = aux.Substring(b, e - b);
                    }
                    dd.data[19] = phoneNumber;
                    original = original.Replace(phoneNumber, "");
  //                  aux = aux.Replace(phoneNumberAux, "");
                    RemoveSpaces(ref original, true, false);
  //                  RemoveSpaces(ref aux, true, false);
                }
                else
                {
                    if (b >= aux.Length)
                        phone = true; 
                }
            }
            return original;
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

//                    if ((p[8] == myReader["URL"].ToString()) || ((p[8] == "") && (myReader["URL"] == DBNull.Value)))
//                    {
//                        count += 1;
//                    }
                    if ((p[8] == "") && (myReader["URL"] == DBNull.Value))
                    {
                        count += 1;
                    }
                    else
                    { // this way will check only the address and not the protocol (ex, http and https)
                        string str1, str2;
                        int i;
                        
                        str1 = p[8];
                        i = str1.IndexOf("://");
                        if (i != -1)
                            str1 = str1.Remove(0, i);

                        str2 = myReader["URL"].ToString();
                        i = str2.IndexOf("://");
                        if (i != -1)
                            str2 = str2.Remove(0, i);

                        if (str1 == str2)
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

            bigger += 1;
            if (bigger > 30000)
            {
                Console.Write("WARNING: To many DealerIDs with almost the same name (the difference is the number)!\n\n");
                AtTheEnd = AtTheEnd + "\nWARNING: To many DealerIDs with almost the same name! (the difference is the number)\n\n";
            }

            temp = p[7];
            if (temp.Length > 14)
                temp = temp.Substring(0, 14);
            for (int i = temp.Length; i < 15; i++)
                temp += "_";
            string DealerID = temp + bigger.ToString();

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
                    select.Value = DealerID;
                    myCommandReader.Parameters.Add(select);
                    myReader = myCommandReader.ExecuteReader();
                    if (myReader.Read())
                    {
                        exist = true;
                        bigger += 1;
                        DealerID = temp + bigger.ToString();
                    }
                    myReader.Close();
                }
                while (exist);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            do
            { // if it is an online deal, locations will be empty. So, DealerDetails must be initialized only to include an empty location into the database. If not, the location is not included and the deal data may not be found because the queries take location into consideration
                location DealerDetails = new location();

                if (locations.CountLocations() != 0)
                    DealerDetails = locations.GetRemoveLocation(0);

                using (SqlCommand myCommandLocation = new SqlCommand("INSERT INTO Locations (DealerID, Latitude, Longitude, StreetName, City, Province, PostalCode, Country, Map, Contact) Values (@DealerID, @Latitude, @Longitude, @StreetName, @City, @Province, @PostalCode, @Country, @Map, @CompanyPhone)", myConnection2))
                {
                    SqlParameter p41a = new SqlParameter();
                    p41a.ParameterName = "@DealerID";
                    p41a.Value = DealerID;
                    myCommandLocation.Parameters.Add(p41a);

                    SqlParameter p9 = new SqlParameter();
                    p9.ParameterName = "@Latitude";
                    temp = DealerDetails.GetLatitude();
                    if (temp == "")
                        p9.Value = DBNull.Value;
                    else
                        p9.Value = temp;
                    myCommandLocation.Parameters.Add(p9);

                    SqlParameter p10 = new SqlParameter();
                    p10.ParameterName = "@Longitude";
                    temp = DealerDetails.GetLongitude();
                    if (temp == "")
                        p10.Value = DBNull.Value;
                    else
                        p10.Value = temp;
                    myCommandLocation.Parameters.Add(p10);

                    SqlParameter p12 = new SqlParameter();
                    p12.ParameterName = "@StreetName";
                    temp = DealerDetails.GetStreetAddress();
                    if (temp == "")
                        p12.Value = DBNull.Value;
                    else
                        p12.Value = temp;
                    myCommandLocation.Parameters.Add(p12);

                    SqlParameter p13 = new SqlParameter();
                    p13.ParameterName = "@City";
                    temp = DealerDetails.GetCity();
                    if (temp == "")
                        p13.Value = DBNull.Value;
                    else
                        p13.Value = temp;
                    myCommandLocation.Parameters.Add(p13);

                    SqlParameter p14 = new SqlParameter();
                    p14.ParameterName = "@PostalCode";
                    temp = DealerDetails.GetPostalCode();
                    if (temp == "")
                        p14.Value = DBNull.Value;
                    else
                        p14.Value = temp;
                    myCommandLocation.Parameters.Add(p14);

                    SqlParameter p15 = new SqlParameter();
                    p15.ParameterName = "@Country";
                    temp = DealerDetails.GetCountry();
                    if (temp == "")
                        p15.Value = DBNull.Value;
                    else
                        p15.Value = temp;
                    myCommandLocation.Parameters.Add(p15);

                    SqlParameter p16 = new SqlParameter();
                    p16.ParameterName = "@Map";
                    temp = DealerDetails.GetMap();
                    if (temp == "")
                        p16.Value = DBNull.Value;
                    else
                        p16.Value = temp;
                    myCommandLocation.Parameters.Add(p16);

                    SqlParameter p17 = new SqlParameter();
                    p17.ParameterName = "@CompanyPhone";
                    temp = DealerDetails.GetContact();
                    if (temp == "")
                        p17.Value = DBNull.Value;
                    else
                        p17.Value = temp;
                    myCommandLocation.Parameters.Add(p17);

                    SqlParameter p18 = new SqlParameter();
                    p18.ParameterName = "@Province";
                    temp = DealerDetails.GetProvince();
                    if (temp == "")
                        p18.Value = DBNull.Value;
                    else
                        p18.Value = temp;
                    myCommandLocation.Parameters.Add(p18);

                    myCommandLocation.ExecuteNonQuery();
                }
            } while (locations.CountLocations() > 0);

            
            SqlCommand myCommandDealer = new SqlCommand("INSERT INTO Dealers (DealerID, Name, URL, FullAddress) Values (@DealerID, @Company, @CompanyURL, @FullAddress)", myConnection2);
//            SqlCommand myCommandDealer = new SqlCommand("INSERT INTO Dealers (DealerID, Name, URL, Latitude, Longitude, FullAddress, StreetName, City, Province, PostalCode, Country, Map, Contact) Values (@DealerID, @Company, @CompanyURL, @Latitude, @Longitude, @FullAddress, @StreetName, @City, @Province, @PostalCode, @Country, @Map, @CompanyPhone)", myConnection2);

            SqlParameter p41b = new SqlParameter();
            p41b.ParameterName = "@DealerID";
            p41b.Value = DealerID;
            myCommandDealer.Parameters.Add(p41b);

            //Everything is NULL in the dealer
            if ((p[8] == "") && (p[13] == ""))
            {
                Console.WriteLine("WARNING: Check Dealer " + DealerID + ". It is completely NULL\n");
                AtTheEnd = AtTheEnd + "WARNING: Check Dealer " + DealerID + ". It is completely NULL\n";
            }

            SqlParameter p5 = new SqlParameter();
            p5.ParameterName = "@Company";
            if (p[7] == "")
                p5.Value = DBNull.Value;
            else
                p5.Value = p[7];
            myCommandDealer.Parameters.Add(p5);

            SqlParameter p6 = new SqlParameter();
            p6.ParameterName = "@CompanyURL";
            if (p[8] == "")
                p6.Value = DBNull.Value;
            else
                p6.Value = p[8];
            myCommandDealer.Parameters.Add(p6);

            SqlParameter p11 = new SqlParameter();
            p11.ParameterName = "@FullAddress";
            if (p[13] == "")
                p11.Value = DBNull.Value;
            else
                p11.Value = p[13];
            myCommandDealer.Parameters.Add(p11);

            myCommandDealer.ExecuteNonQuery();
            
            return (DealerID);
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
                RemoveSpaces(ref p_2, true, true);
                RemoveSpaces(ref p, true, true);
            }
        }

        private bool WebsiteValid(string str, string read)
        {

            string valid = this.SingleDataExtraction(str, read);
            if (valid == "")
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
            tag.data[3] = "(?\"<div class=\\\"side-deal clearfix\\\">\";?\"<a href=\\\"/\";@\"\\\">\";?\"class=\\\"black\\\">\";#\"&$6:\";@\"<\";#\";\";)";
            tag.data[4] = "?\"var DealID =\";@\";\"";
            tag.data[5] = "?\"var AffiliateLinkURL = \\\"\";@\"\\\";\"";
            tag.data[6] = "";
            tag.data[7] = "?\"\\\"og:title\\\" content=\\\"\";@\"\\\"/>\"";
            tag.data[8] = "?\"<a itemprop=\\\"url\\\" href=\\\"\";@\"\\\"\";||?\"\\\"og:url\\\" content=\\\"\";@\"\\\"\"";
            tag.data[9] = "?\"<a class=\\\"dealImage\";?\"<img src=\\\"\"-\"<div class=\\\"whywhat clearfix\\\">\";@\"\\\"\"||?\"\\\"og:image\\\" content=\\\"\";@\"\\\"\"";
            tag.data[10] = "?\"\\\"og:description\\\" content=\\\"\";@\"\\\"/>\";||?\"\var DealName = \\\"\";@\"\\\";\"";
            tag.data[11] = "?\"\\\"og:latitude\\\" content=\\\"\";@\"\\\"\";||?\"&sll=\";@\",\";||?\"&ll=\";@\",\"";
            tag.data[12] = "?\"\\\"og:longitude\\\" content=\\\"\";@\"\\\"\"||?\"&sll=\";?\",\";@\"&\";||?\"&ll=\";?\",\";@\"&\"";
            tag.data[13] = "#\"#2#\";?\"ocations:</strong>\";(?\"</strong><br />\";@2\"<br\")-\"<div class=\\\"divReviews\\\">\",\";;;\"";
            tag.data[14] = "?\"\\\"og:street-address\\\" content=\\\"\";@\"\\\" />\"";
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
            tag.data[43] = "?\"\\\"og:region\\\" content=\\\"\";@\"\\\"\";||?\"\\\"og:locality\";?\"content=\\\"\"-\"/>\";?\",\";@\"\\\" />\";0;#\"\\|\\|\";?\"<div class=\\\"cities\";?$44;?<\"<h3>\";@\"<\"||?\"\\\"og:locality\";?\"content=\\\"\"-\"/>\";?\",\";@\"\\\" />\"||?\"<div class=\\\"cities\";?$44;?<\"<h3>\";@\"<\"";
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
            tag.data[10] = "?\"id=\\\"dealOptionsFancy\";?\"\\\"\";0;($45;#\": \";?\"<h4>\";@\"<\")-\"</div>\",\";&;\"||?\"\\\"og:title\\\" content=\\\"TeamBuy.ca |\";@\"\\\" />\"";
            tag.data[11] = "?\"http://maps.google.ca/maps?\";?\"&sll=\";@\",\"";
            tag.data[12] = "?\"http://maps.google.ca/maps?\";?\"&sll=\";?\",\";@\"&\"";
            tag.data[13] = "#\"#4#\";?\"http://maps.google.ca/maps\"-\"<!-- GOOGLE MAP -->\";?\"http://maps.google.ca/maps\";?<\"<div\";(#\"%city\";?\">\";@\"<\";?\"http://maps.google.ca/maps\";@\"\\\"\";?\"</a\");||#\"#4#\";?\"$(\\\"#more_maps\\\")\";(?\"http://maps.google.ca/maps\";?\"&q=\";@\"\\\"\");||#\"#4#\";?\"<div id=\\\"companyAddress\\\"\";#\"%loca\";?\">\";@\"<\";?\"http://maps.google.ca/maps\";@\"\\\"\";||#\"#4#\";?\"Locations:\";@\"<div style=\\\"float:right\\\">\"";
            tag.data[14] = "?\"<div id=\\\"companyAddress\\\">\";@\"</div>\"";
            tag.data[15] = "";
            tag.data[16] = "";
            tag.data[17] = "#\"Canada\"";
            tag.data[18] = "?\"http://maps.google.ca/maps?\";?<\"\\\"\";@\"\\\"\"";
            tag.data[19] = "?\"<div id=\\\"companyPhone\\\">\";@\"</div>\"";
            tag.data[20] = "?\"id=\\\"dealOptionsFancy\";(?\"href=\";?\"<dd>\";@\"<\")-\"</div>\",\";&;\"||?\"class=\\\"tableDisplay\";?2\"class=\\\"ddValues\\\">\";@\"<\"";
            tag.data[21] = "?\"id=\\\"dealOptionsFancy\";(?\"href=\";?\">\";@\"<\")-\"</div>\",\";&;\"||?\"class=\\\"tableDisplay\";?\"class=\\\"ddValues\\\">\";@\"<\"";
            tag.data[22] = "?\"class=\\\"tableDisplay\";?3\"class=\\\"ddValues\\\">\";@\"/\"";
            tag.data[23] = "?\"id=\\\"dealOptionsFancy\";(?\"%\";?<\">\";@\"<\")-\"</div>\",\";&;\"||?\"class=\\\"tableDisplay\";?3\"class=\\\"ddValues\\\">\";?\"/\";@\"<\"";
            tag.data[24] = "?\"class =\\\"referFriends\";?\"$\";@\"</span>\";||?\"class =\\\"referFriends\";?\"$\";?<\" \";@\"$\"";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "?\"Time Left To Buy\";?\";\\\">\";@\"<\"";
            tag.data[29] = "#\"#2#\"";
            tag.data[30] = "";
            tag.data[31] = "?\"more needed<br/>\";?<\"\\\">\";@\"more needed<br/>\";0;#\"+\";$35;||?\"more buy needed\";?<\"Just\";@\"more buy needed\";0;#\"+\";$35;||?\"Minimum of\";@\"Reached\";||?\"Minimum de\";@\"Atteint\";||?\"<div style=\\\"padding-top:5px;display: table-cell;vertical-align: middle;\\\">\";?6\"</span>\";?<2\">\";?2\" \";@\" \"";
            tag.data[32] = "";
            tag.data[33] = "";
            tag.data[34] = "?\"<span id=\\\"btn-buy_soldout\\\">\";@\"<\"";
            tag.data[35] = "?\"id=\\\"dealOptionsFancy\";(?\"buy-count\\\">\";@\" \")-\"</div>\",\";&;\"||?\"<div style=\\\"padding-top:5px;display: table-cell;vertical-align: middle;\\\">\";?\">\";@\"<br />\"";
            tag.data[36] = "?\"div id=\\\"boxMiddleHighlights\\\">\";?\">\";@\"</ul>\"";
            tag.data[37] = "?\"div id=\\\"boxMiddleDetails\\\">\";?\">\";@\"<div id=\\\"boxBottomDetails\\\"></div>\"";
            tag.data[38] = "?\"<div style=\\\"float:left; height:100%; width: 66%\\\">\";@\"<div style=\\\"float:right\\\">\"";
            tag.data[39] = "?\"<div id=\\\"reviewsWide\\\">\";?\"text-align:justify;\\\">\";@\"<div class=\\\"boxBottom710\\\">\"";
            tag.data[40] = "";
            tag.data[41] = "?2\"/\""; //find DealID/AlternativeID in weblink
            tag.data[42] = ""; //Alternative ID
            tag.data[43] = "";
            tag.data[44] = "";
            tag.data[45] = "?\"\\\"og:title\\\" content=\\\"TeamBuy.ca |\";@\"\\\" />\"";
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
            tag.data[8] = "?\"<br /><a href=\\\"\";@\"\\\"\";||?\"from <a href=\\\"\";@\"\\\"\";||?\"at <a href=\\\"\";@\"\\\"\";||?\"at<a href=\\\"\";@\"\\\"\";||?\"service on <a href=\\\"\"@\"\\\"\";||?\"target=\\\"_blank\\\">site\";?<\"<a href=\\\"\";@\"\\\"\";||?\"at the <a href=\\\"\";@\"\\\"\"";
            tag.data[9] = "?\"\\\"og:image\\\" content=\\\"\";@\"\\\"\"";
            tag.data[10] = "?\"<meta name=\\\"description\\\" content=\\\"\";@\"\\\"/>\"";
            tag.data[11] = "";
            tag.data[12] = "";
            tag.data[13] = "#\"#3#\";?\"var sites = [[\";?<\"var\";@\"]]\";0;#\"/$/\";?\"<!-- End Deal Information -->\";?<\"<br /><a href=\\\"\";?<\"</p>\";@\"<br /><a href=\\\"\"||#\"#3#\";?\"var sites = [[\";?<\"var\";@\"]]\"||#\"#3#\";?\"<strong>Locations\";?\"</p>\";@\"<strong>Reviews\";||#\"#3#\";?\"<strong>Locations\";?\"</p>\";@\"<br /><a href=\\\"\";||#\"#3#\";?\"<!-- End Deal Information -->\";?<\"<strong>Reviews\";?<\"</p>\";@\"<strong>Reviews\"||#\"#3#\";?\"<!-- End Deal Information -->\";?<\"<br /><a href=\\\"\";?<\"<div>\";?\"</p>\n\";@\"<br /><a href=\\\"\"||#\"#3#\";?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";?\"</li></ul>\";@\"<br /><a href=\\\"\";||#\"#3#\";?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";@\"<br /><a href=\\\"\"";
//            tag.data[13] = "?\"var sites = [[\";?<\"var\";@\"]]\"||?\"<strong>Locations\";?\"</p>\";@\"<strong>Reviews\";||?\"<strong>Locations\";?\"</p>\";@\"<br /><a href=\\\"\";||?\"<!-- End Deal Information -->\";?<\"<strong>Reviews\";?<\"</p>\";@\"<strong>Reviews\"||?\"<!-- End Deal Information -->\";?<\"<br /><a href=\\\"\";?<\"<div>\";?\"</p>\n\";@\"<br /><a href=\\\"\"||?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";?\"</li></ul>\";@\"<br /><a href=\\\"\"||?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";@\"<br /><a href=\\\"\"";
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
            tag.data[13] = "#\"#3#\";?\"var sites = [[\";?<\"var\";@\"]]\"||#\"#3#\";?\"<strong>Locations\";?\"</p>\";@\"<strong>Reviews\";||#\"#3#\";?\"<strong>Locations\";?\"</p>\";@\"<br /><a href=\\\"\";||#\"#3#\";?\"<!-- End Deal Information -->\";?<\"<strong>Reviews\";?<\"</p>\";@\"<strong>Reviews\"||#\"#3#\";?\"<!-- End Deal Information -->\";?<\"<br /><a href=\\\"\";?<\"<div>\";?\"</p>\n\";@\"<br /><a href=\\\"\"||#\"#3#\";?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";?\"</li></ul>\";@\"<br /><a href=\\\"\"||#\"#3#\";?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";@\"<br /><a href=\\\"\"";
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
            tag.data[0] = "3;http://www.livingsocial.com/cities/";
            tag.data[1] = "?\"<title>Missing Deal - LivingSocial\";@\">\"";
            tag.data[2] = "?\"<a href=\\\"/cities/\";(?\"<a href=\\\"/cities/\";@\"\\\"\")";
            tag.data[3] = "(?\"<a class=\\\"price\\\" href=\\\"/cities/\";@\"\\\"\";?\" - \";#\"&$6:\";@\"'\";#\";\";)";
            tag.data[4] = "?\"property=\\\"og:url\\\" content=\\\"http://\";?2\"/\";@\"-\"";
            tag.data[5] = "?\"property=\\\"og:url\\\" content=\\\"\";@\"\\\"/>\"";
            tag.data[6] = "";
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
            tag.data[18] = "(?\"class=\\\"directions\\\"><a href=\\\"\";@\"\\\"\"),\"\\|\"";
            tag.data[19] = "";
            tag.data[20] = "";
            tag.data[21] = "?\"<div class=\\\"deal-price \\\">\";?\"</sup>\";@\"</div>\";||?\"<div class=\\\"deal-price \\\">\";@\"<\"   ";
            tag.data[22] = "";
            tag.data[23] = "?\"id=\\\"percentage\\\">\";@\"<\"";
            tag.data[24] = "";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "?\"<span class=\\\"num\\\">\";@\"</div>\"||?\"class=\\\"last\\\">\"-\"sfwt_short_1\";?\">\";@\"</div>\"";
            tag.data[29] = "#\"#2#\"";
            tag.data[30] = "";
            tag.data[31] = "";
            tag.data[32] = "";
            tag.data[33] = "";
            tag.data[34] = "?\"deal-over\";@\"\\\"\";||?\"class=\\\"deal-buy-box buy-now-over\";@\"\\\">\";||?\"class=\\\"deal-buy-box buy-now-soldout\";@\"\\\">\"";
            tag.data[35] = "?\"class=\\\"purchased\\\">\";?\">\";@\"<\"";
            tag.data[36] = "";
            tag.data[37] = "?\"class=\\\"fine-print\\\">\";?\"<p>\";@\"</p>\"";
            tag.data[38] = "?\"div id=\\\"sfwt_full_1\";?\">\";@\"<a class=\\\"sfwt local_ajax\"||?\"<div id=\\\"sfwt_short_1\\\"><p>\";@\"</p> <a class=\\\"sfwt\\\"\"||?\"class=\\\"deal-description\\\">\";@\"</div>\"";
            tag.data[39] = "";
            tag.data[40] = "";
            tag.data[41] = "?2\"/\";@\"-\""; //find DealID/AlternativeID in weblink
            tag.data[42] = ""; //Alternative ID
            tag.data[43] = "";
            tag.data[44] = "#\"/\";?2\"<link href=\\\"http://www.livingsocial.com/\";?\"-\";@\".\";#\"\\\"\"";
            tag.data[45] = "";
            tag.data[46] = "?\"currency_code: \\\"\";@\"\\\"\"";
            tag.data[47] = "";
            tag.data[48] = "";
            tag.data[49] = "?\"<meta name=\\\"keywords\\\" content=\\\"\";@\",\"";
            ListTags.Add(tag);

            tag = new Tags();
            tag.data[0] = "http://www.dealticker.com/product.php?city=Calgary";
            //tag.data[0] = "http://www.dealticker.com/toronto_en_1categ.html";
            tag.data[1] = "?\"no_deal=true\";@\";\" || ?\"404 Not Found\";@\">\"";
            tag.data[2] = "?\"<div id=\\\"city_list\\\"\";(?\"<a href=\\\"http://www.dealticker.com\";@\"\\\"\")-\"<ul class=\\\"lavaLampWithImage\\\" id=\\\"1\\\">\"";
            tag.data[3] = "(?\"<!-- Today's Side Deal -->\";?\"<div id=\";?\"<a href=\\\"\";?3\"/\";@\"\\\"\";?<\"<div id=\";?2\">\";#\"&$6:\";@\"<\";#\";\";)-\"<td valign=\\\"bottom\\\" style=\\\"height: 170px;\\\">\"";
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
            tag.data[38] = "?\"<div id=\\\"description\\\"\";?\">\";@\"</span></p></div>\"||?\"<div id=\\\"description\\\"\";?\">\";@\"WHAT PEOPLE ARE SAYING<\"||?\"<div id=\\\"description\\\"\";?\">\";@\"</span></div></div>\"||?\"<div id=\\\"description\\\"\";?\">\";@\"<div style=\\\"border\"";
            tag.data[39] = "?\">WHAT PEOPLE ARE SAYING<\";?\">\";@\"</span></div></div>\"";
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

            tag = new Tags();
            tag.data[0] = "http://www.takeitandgo.ca/localdeals/show/Toronto";
            tag.data[1] = "?\"404 Not Found\";@\">\"||?\"HTTP Status 404\";@\"</\"";
            tag.data[2] = "?\"<div id=\\\"citybox\";(?\"<a href='\";@\"'\")-\"<!-- footer End -->\"";
            tag.data[3] = "?\"<!-- related deals -->\";(?\"<p><a href=\\\"https://takeitandgo.ca/localdeals/\";@\"\\\"\")";
            tag.data[4] = "?\"https://takeitandgo.ca/localdeals/show/\";?\"/\"@\"&\"";
            tag.data[5] = "?\"share this deal\";?2\"https://\";?<\" \";@\"\\\"\"";
            tag.data[6] = "";
            tag.data[7] = "?\"<div class=\\\"dealbox\\\">\";?\">\"@\"<\"";
            tag.data[8] = "?\"<div class=\\\"infobox\";?\"<a href='\";@\"'\"";
            tag.data[9] = "?\"<link rel=\\\"image_src\";?\"href=\\\"\";@\"\\\"\"";
            tag.data[10] = "?\"<meta name=\\\"Description\";?\"content=\\\"\";@\"\\\"\"";
            tag.data[11] = "";
            tag.data[12] = "";
            tag.data[13] = "#\"#6#\";?\"<div class=\\\"infobox\";?\"</strong>\"-\">The Fine Print<\";@\"</div>\"";
            tag.data[14] = "";
            tag.data[15] = "";
            tag.data[16] = "";
            tag.data[17] = "?\"<div id=\\\"citybox\";?$44;?<\"<strong>\";@\"<\"";
            tag.data[18] = "";
            tag.data[19] = "";
            tag.data[20] = "";
            tag.data[21] = "?\"div class=\\\"price_info\";?\"class=\\\"large\\\">\"-\"<div id=\\\"buynow\";@\"<\"";
            tag.data[22] = "";
            tag.data[23] = "?\">savings<\";?\"<p>\";@\"</p>\"";
            tag.data[24] = "?\"<p>Refer a Friend and Get Paid \";@\"(\"";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "";
            tag.data[29] = "#\"#3#\";?2\"StartCountDown(\\\"time_remaining\\\",\\\"\"@\"\\\"\";||?\"StartCountDown(\\\"time_remaining\\\",\\\"\"@\"\\\"\"";
            tag.data[30] = "?\">quantity left</h5>\";@\"</p>\"";
            tag.data[31] = "";
            tag.data[32] = "";
            tag.data[33] = "?\"function StartCountDown\";?\"images/dealclosed\"-\"function Calcage\";@\"\\\"\"";
            tag.data[34] = "";
            tag.data[35] = "";
            tag.data[36] = "?\">Why You Should Take It And Go<\";?\">\";@\">Deal Description<\"";
            tag.data[37] = "?\">The Fine Print<\";?\">\";@\">Why You Should Take It And Go<\"";
            tag.data[38] = "?\">Deal Description<\";?\">\";@\">Reviews<\"||?\">Deal Description<\";?\">\";@\">Our Promise and Guarantee<\"";
            tag.data[39] = "?\">Reviews<\";?\">\";@\">Our Promise and Guarantee<\"";
            tag.data[40] = " ";
            tag.data[41] = "?2\"/\""; //find DealID/AlternativeID in weblink
            tag.data[42] = "?\"https://takeitandgo.ca/localdeals/show/\";?\"/\";@\"&\""; //Alternative ID
            tag.data[43] = "";
            tag.data[44] = "?\"topcity\\\").innerHTML=\\\"\";@\"\\\"\"";
            tag.data[45] = "";
            tag.data[46] = "?\"c$\";?\"<\";#\"CAD\"";
            tag.data[47] = "";
            tag.data[48] = "";
            tag.data[49] = "?\"div id=\\\"header\";?\"alt=\\\"\";@\".\"";
            ListTags.Add(tag);

                        tag = new Tags();
                        tag.data[0] = "10;http://www.groupon.com/greater-toronto-area/";
                        tag.data[1] = "?\"<title>Oops! That page doesn't exist\";@\">\"";
                        tag.data[2] = "?\"id='state_Country\";(?\"<li\";?\"<a href=\\\"\";@\"\\\"\";#\"all\")-\"<ul class='country'>\";0;(?\"<li class='canada\";?\"<a href=\\\"\";@\"\\\"\";#\"all\")-\"<ul class='country'>\"";
                        //                        tag.data[2] = "?\"id='state_Country\";(?\"<li>\";?\"<a href=\\\"\";@\"\\\"\";#\"all\")-\"<div class='announcement\";0;(?\"<li class='area'>\";?\"<a href=\\\"\";@\"\\\"\";#\"all\")-\"<div class='announcement\";0;(?\"<li class='canada'>\";?\"<a href=\\\"\";@\"\\\"\";#\"all\")-\"<div class='announcement\";0;(?\"<li class='country'>\";?\"<a href=\\\"\";@\"\\\"\";#\"all\")-\"<div class='announcement\"";
                        tag.data[3] = "(?\"<div class=\\\"inner\\\">\";?\"<a href=\\\"\";@\"\\\"\");||?\"class='side_deal clearfix\";(?3\"<a href=\\\"\";@\"\\\"\"-\"</li>\";?\"<span>\";#\"&$6:\";@\"<\";#\";\";?\"class=\\\"number\\\">\";#\"&$35:\";@\"<\";#\";\";||?3\"<a href=\\\"\";@\"\\\"\")-\"id=\\\"groupon_promise\"";
                        tag.data[4] = "?\"class='pledges\";(?\"class='description\";?\"<a href=\\\"https\";?\"pledge_id=\";@\"\\\"\")-\"class='integrated_side_deals\",\";&;\";||?\"pledge_id=\";@\"\\\"\";||?\"Groupon.currentDivision = \\\"\";@\"\\\"\"";
                        tag.data[5] = "?\"class='url'\";?\">\";@\"<\"";
                        tag.data[6] = "";
                        tag.data[7] = "?\"class='name'>\";@\"<\"";
                        tag.data[8] = "?\"class='company_links\";?\"<a href=\\\"\"-\"</ul>\";@\"\\\"\"";
                        tag.data[9] = "?\"class='photo'\";?\">\";@\"<\"";
                        tag.data[10] = "?\"class='pledges\";(?\"class='description\";?\"<a href=\\\"https\";?\"class=\\\"title\\\">\";@\"<\")-\"class='integrated_side_deals\",\";&;\"||?\"Groupon.currentDeal.title = \\\"\";@\"\\\";\"";
                        tag.data[11] = "?2\"class='locations\";?\"data-lat='\";@\"'\"";
                        tag.data[12] = "?2\"class='locations\";?\"data-lng='\";@\"'\"";
                        tag.data[13] = "#\"#5#\";?2\"class='locations\";(?\"class='address'>\";@\"</div>\"),\"\\|\"";
                        tag.data[14] = "";
                        tag.data[15] = "";
                        tag.data[16] = "";
                        tag.data[17] = "?\"<ul id='state_chooser\";?$44;?<\"'canada state'>Alberta\";?\"<li class='canada'\";?<\"='\";@\"'\";||#\"United States\"";
                        tag.data[18] = "(#\"=\";?\"data-lat='\";@\"'\";#\",\";?\"data-lng='\";@\"'\")-\"class='reviews\",\"\\|\"";
                        tag.data[19] = "";
                        tag.data[20] = "?\"class='pledges\";(?\"class='description\";?\"<a href=\\\"https\";?\"class='value\";?\"$\";@\"<\")-\"class='integrated_side_deals\",\";&;\";||?\"id='deal_discount\";?\"$\"-\"</div>\";@\"<\"";
                        tag.data[21] = "?\"class='pledges\";(?\"class='description\";?\"<a href=\\\"https\";?\"class='amount'>\";@\"<\")-\"class='integrated_side_deals\",\";&;\";||?\"class='price'\";?\"$\";@\"<\"";
                        tag.data[22] = "?\"class='pledges\";(?\"class='description\";?\"<a href=\\\"https\";?\"class='value\";?2\"$\";@\"<\")-\"class='integrated_side_deals\",\";&;\";||?\"class='save\";?\"$\";@\"<\"";
                        tag.data[23] = "?\"class='pledges\";(?\"class='description\";?\"<a href=\\\"https\";?\"class='value\";?\"%\";?<\">\";@\"%\")-\"class='integrated_side_deals\",\";&;\";||?\"class='discount\";?\"%\";?<\">\";@\"%\"";
                        tag.data[24] = "";
                        tag.data[25] = "";
                        tag.data[26] = "?\"data-deadline='\";@\"'\"";
                        tag.data[27] = "";
                        tag.data[28] = "";
                        tag.data[29] = "#\"#4#\"";
                        tag.data[30] = "";
                        tag.data[31] = "";
                        tag.data[32] = "?\"Groupon.currentDeal.sold_out =\";@\";\"";
                        tag.data[33] = "";
                        tag.data[34] = "?\"Groupon.currentDeal.status = \\\"c\";@\"\\\"\"";
                        tag.data[35] = "?\"class='pledges\";(?\"class='description\";?\"<a href=\\\"https\";?\"class='status'>\";?\">\";@\"<\")-\"class='integrated_side_deals\",\";&;\"||?\"class='status'>\";?\">\";@\"<\"";
                        tag.data[36] = "?\"class='brief_summary\";?\"<p>\";@\"</div>\"||?\"class='highlights\";?\"<p>\";@\"</div>\"||?\"class='reservations\";?\">What It's Worth<\";?\">\";@\"</div>\"";
                        tag.data[37] = "?\"name='fine_print\";?\"<ul>\";?<2\">\";@\"</div>\"||?\"class='border'>The Fine Print\";@\"</div>\"";
                        tag.data[38] = "?\"class='article description'>\";?\">\";@\"class='btn_discussion_writeup_join\"||?\"class='pitch_content\";?\">\";@\"</div>\"";
                        tag.data[39] = "?\"class='reviews'>\";?2\">\";@\"class=\\\"modal_window\"";
                        tag.data[40] = "";
                        tag.data[41] = "?\"deals/\";@\"?\";||?\"deals/\" "; //find DealID/AlternativeID in weblink
                        tag.data[42] = "?\"Groupon.currentDeal.permalink = \\\"\";@\"\\\";\""; //Alternative ID
                        tag.data[43] = "";
                        tag.data[44] = "?\"Groupon.currentDivision = \\\"\";@\"\\\"\"";
                        tag.data[45] = "";
                        tag.data[46] = "?\"c$\";?\"<\";#\"CAD\";||#\"USD\"";
                        tag.data[47] = "";
                        tag.data[48] = "";
                        tag.data[49] = "?\"property='og:site_name\";?<2\"='\";@\"'\"";
                        ListTags.Add(tag);


                        tag = new Tags();
                        tag.data[0] = "http://www.giltcity.com/newyork";
                        tag.data[1] = "?\"id=\\\"c_error\";@\"<\"";
                        tag.data[2] = "(?\"class=\\\"city\\\"\";?\"<a href=\\\"\";@\"\\\"\")";
                        tag.data[3] = "(?\"class=\\\"offer-info\";?\"<a href=\\\"\";@\"\\\"\")";
                        tag.data[4] = "(?\"data-package-id=\\\"\";@\"\\\"\"),\";&;\"";
                        tag.data[5] = "?\"canonical\\\" href=\\\"\";@\"\\\"\"";
                        tag.data[6] = "?\"offer-category-breadcrumb\";?\">Services<\"-\"</ul>\";?\">\";#\"Mobile\"||?\"offer-category-breadcrumb\";?\"<li>\";@\"</ul>\"";
                        tag.data[7] = "?\"section-heading\\\">About\";@\"</\";||?\"offer-name\\\">\";@\"</\"";
                        tag.data[8] = " ";
                        tag.data[9] = "?\"og:image\";?\"=\\\"\";@\"\\\"\"";
                        tag.data[10] = "?\"id=\\\"package-container\";(?<\"class=\\\"h1 offer-name\\\">\";@\"</\";#\" - \";0;?\"class=\\\"pkg-title\\\">\";@\"<\"),\";&;\"";
                        tag.data[11] = " ";
                        tag.data[12] = " ";
                        tag.data[13] = "#\"#5#\";?$44;?\">\";@\"</div>\"";
                        tag.data[14] = "(?\"class=\\\"street-address\\\">\";@\"</\"),\";&;\"";
                        tag.data[15] = "(?\"class=\\\"locality\\\">\";@\"</\"),\";&;\"";
                        tag.data[16] = "(?\"class=\\\"postal-code\\\">\";@\"</\"),\";&;\"";
                        tag.data[17] = "#\"United States\"";
                        tag.data[18] = " ";
                        tag.data[19] = " ";
                        tag.data[20] = "(?\"class=\\\"discount accent\\\">\";?\"$\";@\"</\"||?\"class=\\\"pkg-buy\";?\"class=\\\"pkg-price\";#\"$\"),\";&;\"";
                        tag.data[21] = "(?\"class=\\\"pkg-buy\";?\"class=\\\"buy-sold-out\\\">\"-\"class=\\\"pkg-price\";@\"!\"||?\"class=\\\"pkg-buy\\\">\";?\"$\"-\"</a>\";@\"<\"||?\"class=\\\"pkg-buy\\\">\";?\"FREE\"-\"</a>\";#\"0\"),\";&;\"";
                        tag.data[22] = " ";
                        tag.data[23] = "(?\"class=\\\"discount accent\\\">\";?\"%\";?<\">\";@\"%\"||?\"class=\\\"pkg-buy\";?\"class=\\\"pkg-price\";#\"$\"),\";&;\"";
                        tag.data[24] = " ";
                        tag.data[25] = " ";
                        tag.data[26] = " ";
                        tag.data[27] = " ";
                        tag.data[28] = "?\"class=\\\"offer-ends\";?\"Ends in:\"-\"</p>\";@\"</\"";
                        tag.data[29] = "#\"#5#\"?\"class=\\\"offer-ends\";?\"Ends on:\"-\"</p>\";?\",\";@\"</\"||#\"#2#\"";
                        tag.data[30] = " ";
                        tag.data[31] = " ";
                        tag.data[32] = " ";
                        tag.data[33] = " ";
                        tag.data[34] = " ";
                        tag.data[35] = " ";
                        tag.data[36] = "?\"id=\\\"package-container\";(?<\"class=\\\"vendor-blurb\";?\">\";@\"</\";#\" Includes: \";0;?\"class=\\\"pkg-includes\";?\"<ul>\";@\"</section>\"),\";&;\"";
                        tag.data[37] = "(?\"class=\\\"what-to-know\";?\"<ul\";?\">\";@\"</section>\"),\";&;\"";
                        tag.data[38] = "(?\"class=\\\"what-we-love\";?\"<ul\";?\">\";@\"</section>\"),\";&;\"";
                        tag.data[39] = "?\"id=\\\"the-review\";?\">\";@\"</div>\";0;?$45;?\">\";@\"</div>\";0;?$47;?\">\";@\"</div>\"||?\"id=\\\"the-review\";?\">\";@\"</div>\";0;?$47;?\">\";@\"</div>\"||?\"id=\\\"the-review\";?\">\";@\"</div>\";0;?$45;?\">\";@\"</div>\"||?\"id=\\\"the-review\";?\">\";@\"</div>\"";
                        tag.data[40] = " ";
                        tag.data[41] = "?2\"/\"";
                        tag.data[42] = "?\"Gilt.City.offerUrlKey = '\";@\"'\"";
                        tag.data[43] = "(?\"class=\\\"region\\\">\";@\"</\"),\";&;\"";
                        tag.data[44] = "?\"class=\\\"accent\\\">Locations\";?<\"#\";#\"div id=\\\"\";@\"\\\"\"";
                        tag.data[45] = "?\"class=\\\"accent\\\">Press<\";?<\"#\";#\"div id=\\\"\";@\"\\\"\"";
                        tag.data[46] = "#\"USD\"";
                        tag.data[47] = "?\"class=\\\"accent\\\">The Buzz<\";?<\"#\";#\"div id=\\\"\";@\"\\\"\"";
                        tag.data[48] = " ";
                        tag.data[49] = "?\"baseURI = 'http://www.\";@\".\"";
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
            baseaddress.Add("https://takeitandgo.ca/localdeals/$");
            baseaddress.Add("http://www.groupon.com$");
            baseaddress.Add("http://www.giltcity.com$");

            List<string> DontHandleFirstPage = new List<string>();
            DontHandleFirstPage.Add("http://www.teambuy.ca/toronto");
            DontHandleFirstPage.Add("http://www.groupon.com/greater-toronto-area/");
            DontHandleFirstPage.Add("http://www.giltcity.com/newyork");

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
            using (SqlCommand myCommand = new SqlCommand("MERGE Deals.dbo.DealsEnded USING (SELECT * FROM [Deals].[dbo].[DealsList] where ExpiryTime < @CURRENT_TIME) as source on (Deals.dbo.DealsEnded.DealID = source.dealID) WHEN not matched then insert (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, Saved, Discount, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews, DealSite, Currency) Values (source.Website, source.DealID, source.DealLinkURL, source.Category, source.Image, source.Description, source.DealerID, source.RegularPrice, source.OurPrice, source.Saved, source.Discount, source.PayOutAmount, source.PayOutLink, source.ExpiryTime, source.MaxNumberVouchers, source.MinNumberVouchers, source.PaidVoucherCount, source.DealExtractedTime, source.Highlights, source.BuyDetails, source.DealText, source.Reviews, source.DealSite, source.Currency);", myConnection))
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

            int numThreads = ListTags.Count;
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            int toProcess = numThreads;

            Console.WriteLine(DateTime.Now);

//            for (int i = 0; i < ListTags.Count(); i++)
            {
                int i = 8;
                if (i == 6)
                    i += 1;
                string website = ListTags.ElementAt(i).data[0];
                if ((website.Length > 6) && (website.Substring(0, 6) != "$STOP$"))
                {
                    if (website.IndexOf(';') != -1)
                        website = website.Substring(website.IndexOf(';') + 1);
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
  /*          myConnection = new SqlConnection("server=MEDIACONNECT-PC\\MCAPPS; Trusted_Connection=yes; database=Deals; connection timeout=15");
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

            myConnection.Close();*/

//            string myChoice = Console.ReadLine();
        }
    }
}
