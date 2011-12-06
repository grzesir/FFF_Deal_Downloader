using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Globalization;

namespace PlayingWithCsharp
{
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
        public string[] data = new string[44];
        public Tags()
        {
            for (int i = 0; i < 44; i++) data[i] = "";
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
        public int GetTimes()
        {
            return (this.times);
        }
        public void SetTimes(int i)
        {
            this.times = i;
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
        public static keywords GetSearchString(string str, ref int c1)
        {
            keywords k = new keywords();
            if (c1 > str.Length)
            {
                Console.WriteLine("Mistake at the end of Search keyword (?).");
                k.SetTimes(-1);
                return(k);
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
                if ((str[c2] >= '0') && (str[c2] <= '9'))
                {
                    k.SetKeyword(str.Substring(c1, 2));
                    c1 += 1;
                }
                else
                    k.SetKeyword(str.Substring(c1, 1));

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
                        k.SetTimes(-1);
                        return(k);
                    }
                } while (str[c2 - 1] == '\\');
                k.SetKeyword(str.Substring(c1,c2-c1));
                k.SetKeyword(k.GetKeyword().Replace("\\\"", "\""));
                if (k.GetTimes() == 0) k.SetTimes(1);
                k.SetType('?');
                c1 = c2 + 1;
            }
            else
            {
                Console.WriteLine("Error in Search tag (?) format. Probably missing \" at " + c1 + " character.");
                k.SetTimes(-1);
            }
            return (k);
        }

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
        public static keywords GetEndString(string str, ref int c1)
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
                k.SetKeyword(str.Substring(c1, c2 - c1));
                k.SetKeyword(k.GetKeyword().Replace("\\\"", "\""));
                if (k.GetTimes() == 0) k.SetTimes(1);
                k.SetType('@');
                c1 = c2 + 1;
            }
            else
            {
                Console.WriteLine("Error in End tag (@) format. Probably missing \" at " + c1 + " character.");
                k.SetTimes(-1);
            }
            return (k);
        }

        public static string GetConstant(string str, ref int c1)
        {
            string str_new = "";
            if (c1 > str.Length)
            {
                Console.WriteLine("Mistake at the end of #constant/@- keyword.");
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
                        return ("{:-(");
                    }
                } while (str[c2 - 1] == '\\');
                str_new = str.Substring(c1, c2-c1);
                c1 = c2 + 1;
            }
            else
            {
                Console.WriteLine("Error in #Constant tag format. Probably missing \" at " + c1 + " character.");
                return ("{:-(");
            }
            return (str_new);
        }

        public static string GetRepeatedOperation(string str, ref int c1, ref string read)
        {
            string str_new = "";
            if (c1 > str.Length)
            {
                Console.WriteLine("Mistake at the end of ( keyword.");
                return ("{:-(");
            }
            int c2 = c1 - 1;
            do
            {
                c2 = str.IndexOf(')', c2 + 1);
                if (c2 == -1)
                {
                    Console.WriteLine("Missing ). Can't go on.");
                    return ("{:-(");
                }
            } while (str[c2 - 1] == '\\');
            str_new = str.Substring(c1, c2 - c1);
            c2 += 1;

            if ((c2 < str.Length) && (str[c2] == '-'))
            {
                c2 += 1;
                keywords aux = MainOperations.GetEndString(str, ref c2);
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
    }
    
    public class Extraction
    {
        Tags oneWebsite;
        string baseAddress;
        public Extraction(Tags oneWebsite, string baseAddress)
        {
            this.oneWebsite = oneWebsite;
            this.baseAddress = baseAddress;
        }

        public string SingleDataExtraction(string str, string read)
        {
            int aux = 0;
            return (SingleDataExtraction(str, read, ref aux));
        }

        public string SingleDataExtraction(string str, string read, ref int position)
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
            while ((c < str.Length) || (op_rep))
            {
                if ((op_rep) && (c >= str.Length))
                {
                    if (read_pos != -1)
                    {
                        c = 0;
                        temp_result = temp_result + result + "\n";
                        result = "";
                    }
                    else
                    {
                        op_rep = false;
                        c = temp_c;
                        str = temp_str;
                        if (result != "")
                        {
                            temp_result = temp_result + result;
                            RemoveTags(ref temp_result);
                            RemoveAmpersand(ref temp_result);
                        }
                        result = temp_result;
//                        return (temp_result);
                    }
                }

                else if (str[c] == '|')
                {
                    if (((c + 1) < str.Length) && (str[c + 1] == '|') && (!op_rep))
                    {
                        RemoveTags(ref result);
                        if (result == "http://")
                            result = "";
                        if (result != "")
                        {
                            c = str.Length;
                            continue;
                        }
                        read_pos = pos_ini;
                        err_not_found = "";
                        c += 2;
                        search = new keywords();
                        end = new keywords();
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Mistake at the Tag/Keyword (|).");
                        return ("{:-(");
                    }
                }

                // getting the operations that must be executed continuasly till the end of the html page
                else if (str[c] == '(')
                {
                    string r;
                    c += 1;
                    r = MainOperations.GetRepeatedOperation(str, ref c, ref read);
                    op_rep = true;
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
                // getting the constant string that must be added to the information we are looking for
                else if (str[c] == '#')
                {
                    string r;
                    c += 1;
                    r = MainOperations.GetConstant(str, ref c);
                    if (r == "{:-(")
                    {
                        return ("{:-("); // ends the Thread?
                    }
                    result = result + r;
                    continue;
                }
                // getting string that is located before the information we need on html page
                else if (str[c] == '?')
                {
                    while (search.GetTimes() > 0)
                    {
                        search.SetTimes(search.GetTimes() - 1);
                        // search for the current keyword on html page. Variable search (Keywords) will be overwritten 
                        if (search.GetDirection() == '>')
                        {
                            if (read_pos == -1)
                                break;
                            read_pos = read.IndexOf(search.GetKeyword(), read_pos);
                            if (read_pos == -1)
                            {
                                c = str.IndexOf('|',c);
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
                            if (read_pos == -1)
                                break;
                            read_pos = read.LastIndexOf(search.GetKeyword(), read_pos);
                            if (read_pos == -1)
                            {
                                c = str.IndexOf('|',c);
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
                                read_pos += search.GetKeyword().Length;
                        }
                    }
                    if ((c >= str.Length) || (read_pos == -1))
                        continue;
                    c += 1;
                    search = new keywords();
                    search = MainOperations.GetSearchString(str, ref c);
//                    if (search.GetTimes() == -1)
//                    {
//                        return; // ends the Thread?
//                    }

                    if ((c < str.Length) && (str[c] == '-'))
                    {
                        c += 1;
                        string tillHere = MainOperations.GetConstant(str, ref c);
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
                    end = MainOperations.GetEndString(str, ref c);
                    while (search.GetTimes() > 0)
                    {
                        search.SetTimes(search.GetTimes() - 1);
                        // search for the current keyword on html page. Variable search (Keywords) will be overwritten 
                        if (search.GetDirection() == '>')
                        {
                            if (read_pos == -1)
                                break;
                            read_pos = read.IndexOf(search.GetKeyword(), read_pos);
                            if (read_pos == -1)
                            {
                                c = str.IndexOf('|',c);
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
                            if (read_pos == -1)
                                break;
                            read_pos = read.LastIndexOf(search.GetKeyword(), read_pos);
                            if (read_pos == -1)
                            {
                                c = str.IndexOf('|',c);
                                position = read_pos;
                                if (c == -1)
                                    c = str.Length;
                                err_not_found = "The data you are looking for can not be found in the HTML file";
  //                              Console.WriteLine("The data you are looking for can not be found in the HTML file");
                                break;
                                //                                return("{:-("); //ends the Thread?
                            }
                            else
                                read_pos += search.GetKeyword().Length;
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
                            c = str.IndexOf('|', c);
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
            result = result.Replace("<td>", "");
            result = result.Replace("</td>", "; ");
            result = result.Replace("<i>", "");
            result = result.Replace("</i>", "");
            result = result.Replace("<tr>", "");
            result = result.Replace("</tr>", "\n");
            result = result.Replace("<strong>", "");
            result = result.Replace("</strong>", "");
            result = result.Replace("</div>", "");
            result = result.Replace("</span>", "");
            result = result.Replace("</a>", "");
            result = result.Replace("<b>", "\n");
            result = result.Replace("</b>", "\n\n");
            result = result.Replace("<em>", "\n");
            result = result.Replace("</em>", "\n\n");
            result = result.Replace("%20", " ");

            string htmlTag = "<div";
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

            htmlTag = "<li";
            i = result.IndexOf(htmlTag);
            while (i != -1)
            {
                int j = result.IndexOf(">", i + 1);
                if (j != -1)
                {
                    if (j > i + 3)
                        result = result.Replace(result.Substring(i + 3, j - i + 3), " ");
                    i = result.IndexOf(htmlTag, i + 1);
                }
                else
                {
                    result = result.Replace(result.Substring(i, result.Length - i), " ");
                    i = result.IndexOf(htmlTag, i + 1);
                }
            }

            RemoveSpaces(ref result);
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
            RemoveSpaces(ref result);
        }

        
        private void RemoveSpaces(ref string temp_result)
        {
            int b = 0;
            int e = temp_result.Length - 1;
            if ((e == 0) && (temp_result[0] != ' ') && (temp_result[0] != '\n') && (temp_result[0] != '\t'))
                return;
            while ((b <= e) && ((temp_result[b] == ' ') || (temp_result[b] == 160) || (temp_result[b] == '\n') || (temp_result[b] == '\t') || (isPunctuation(temp_result[b]))))
            {
                b+=1;
            }
            while ((e > b) && ((temp_result[e] == ' ') || (temp_result[e] == '\n') || (temp_result[e] == '\t')))
            {
                e -= 1;
            }
            if (e > b)
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
                Console.WriteLine("ERROR: Exception reading from webpage " + URL);
                strData = "ERROR: Exception reading from webpage";
            }
            return strData;
        }

        // Thread responsible for extracting the all of the cities links for a given website
        public void ExtractingCities()
        {
            string read;
            string AtTheEnd = "";

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
            if (temp == "{:-(")
            {
                Console.WriteLine("ERROR: Couldn't find cities in website " + this.oneWebsite.data[0]);
                writer.WriteLine("ERROR: Couldn't find cities in website " + this.oneWebsite.data[0]);
                writer.Close();
                return;
            }
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
                string part_URL = item;
                string URL = "";
                int tries = 3;
                Boolean FirstTime = true;
                do
                {   
                    string DealID = "";
                    int i;
                    Tags DealData = new Tags();

                    if (SideDeals.Count() > 0)
                    {
                        part_URL = SideDeals.ElementAt(0);
                        EvaluatedSideDeals.Add(part_URL);
                        SideDeals.RemoveAt(0);
                    }

      //                             URL = "http://www.dealticker.com/product.php/product_id/16899";
                    URL = baseAddress.Replace("$", part_URL);
                    // opening Website
                    read = DownloadData(URL);

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
                            str = this.oneWebsite.data[3];
                            temp = this.SingleDataExtraction(str, read);
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
                                                    search = MainOperations.GetSearchString(str, ref c);
                                                    while ((c < str.Length) && ((str[c] == ' ') || (str[c] == ';')))
                                                    {
                                                        c += 1;
                                                    }
                                                }
                                                if ((c < str.Length) && (str[c] == '@'))
                                                {
                                                        c += 1;
                                                        end = MainOperations.GetEndString(str, ref c);
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

                            if (FirstTime)
                            {
                                FirstTime = false;
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

                                        Console.WriteLine(baseAddress.Replace("$", "") + " - " + item + " \tDealID - " + DealID);

                                        // Get the data / write to file
                                        //             line = baseAddress.Replace("$","") + " | " + DealID + " | ";
                                        for (int j = 5; j <= 40; j++)
                                        {
 //                                           Console.Write(j + " ");
                                            if (j == 14)
                                            {
                                                Console.Write("");
                                            }
                                            int read_pos = 0;
                                            str = this.oneWebsite.data[j];
                                            temp = this.SingleDataExtraction(str, read, ref read_pos);
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
                                                    temp = this.SingleDataExtraction(str, read, ref read_pos);
                                                    has = temp.IndexOf("youtube.com");
                                                    if (has == -1)
                                                        has = temp.IndexOf("wikipedia");
                                                }
                                            }
                                            // end Data not expected.
                                            temp = temp.Replace("\n", "; ");
                                            while (temp.IndexOf("; ;") != -1)
                                                temp = temp.Replace("; ;", "; ");
                                            DealData.data[j] = temp;
                                        }
                                        // extract province
                                        str = this.oneWebsite.data[43];
                                        str = this.SingleDataExtraction(str, read);
                                        DealData.data[43] = str;

                                        listOfDeals.Add(DealData);
                                    }
                                }
                            }
                        }
                    }
                    if ((SideDeals.Count == 0) && (TryLater.Count != 0))
                    {
                        if (tries > 0)
                        {
                            SideDeals.Add(TryLater.ElementAt(0));
                            TryLater.RemoveAt(0);
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
                Console.WriteLine(e.ToString());
            }

   //         int cont = 0;

// Store the data into SQL Database. Clean and handle the data, if needed
            foreach (Tags dd in listOfDeals)
            {
//                cont += 1;
//                if (cont == 198)
//                    Console.WriteLine("");
//                Console.WriteLine(cont + " " + listOfDeals.Count);
                string line = "";

// Data Handling

                for (int i = 1; i < 44; i++)
                {
                    if (dd.data[i] == "{:-(")
                        dd.data[i] = "";
                    RemoveSpaces(ref dd.data[i]);
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

                dd.data[8] = dd.data[8].Replace("www.", ""); // let all companie's URL with the same format, i.e., without "www." and the last "/"
                if ((dd.data[8].Length>=1) && (dd.data[8][dd.data[8].Length-1] == '/')) 
                    dd.data[8] = dd.data[8].Substring(0, dd.data[8].Length-1);
                if (dd.data[8].IndexOf("youtube.com") != -1)
                    dd.data[8] = "";

                if ((dd.data[15].Length >= 5) && (dd.data[15].Substring(0, 5).ToLower() == "http:"))
                    dd.data[15] = "";
                if ((dd.data[15].Length >= 4) && (dd.data[15].Substring(0, 4).ToLower() == "www."))
                    dd.data[15] = "";

                if ((dd.data[18].Length == 30) && (dd.data[18] == "http://maps.google.com/maps?q="))
                    dd.data[18] = "";

                if (dd.data[0] == "http://www.dealticker.com/toronto_en_1categ.html")
                {
                    transferEmails(ref dd.data[15], ref dd.data[19]);
                    if ((dd.data[15] != "") && ((dd.data[15][0] == '(') || ((dd.data[15][0] >= '0') && (dd.data[15][0] <= '9'))))
                    {
                        // Contacts are in the wrong place. Moving them from City to Contact
                        dd.data[19] = dd.data[19] + dd.data[15] + "; ";
                        dd.data[15] = "";
                        // streetName must be null??
           //             dd.data[14] = "";
                    }
                }

                if (dd.data[0] == "http://www.teambuy.ca/toronto")
                {
                    if (dd.data[14] != "")
                    {
                        dd.data[14] = dd.data[14].Replace("(map)", "");
                        RemoveSpaces(ref dd.data[14]);
                    }
                    if ((dd.data[18] != "") && (dd.data[13] == "") && (dd.data[14] == ""))
                    {
                        dd.data[18] = "";
                    }
                    // create as many rows as needed and start extracting address data from googlemaps
                }
                
                Boolean phone = false;
                for (int i = 13; i <= 15; i++)
                {
                    string aux1 = dd.data[i].ToLower();
                    string aux = aux1;
                    if (!((i == 13) && (dd.data[0] == "http://www.dealfind.com/toronto")))
                    {
                        aux = ExtractPhone(aux, dd, ref phone);
                    }
                    aux = RemoveWebLinks(aux);
                    if (aux == "include photo")
                        dd.data[19] = dd.data[i] + ", " + dd.data[19];
                    aux = aux.Replace("to redeem voucher,", "");
                    aux = aux.Replace("to redeem voucher", "");
                    aux = aux.Replace("please visit:", "");
                    aux = aux.Replace("please visit", "");
                    aux = aux.Replace("redeem online by clicking the \"redemption\" link on your voucher", "");
                    aux = aux.Replace("Redeem online by clicking \"Redemption\" link on your voucher", "");
                    aux = aux.Replace("online redemption:", "");
                    aux = aux.Replace("online redemption", "");
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
                    aux = aux.Replace("mobile", "");
                    aux = aux.Replace("or by email:", "");
                    aux = aux.Replace("or by email", "");
                    aux = aux.Replace("by emailing:", "");
                    aux = aux.Replace("by emailing", "");
                    aux = aux.Replace("by email:", "");
                    aux = aux.Replace("by email", "");
                    aux = aux.Replace("for inquiries,", "");
                    aux = aux.Replace("for inquiries", "");
                    aux = aux.Replace("please call:", "");
                    aux = aux.Replace("please call", "");
                    aux = aux.Replace("call:", "");
                    aux = aux.Replace("call ", " ");
                    aux = aux.Replace("call\n", "\n");
                    aux = aux.Replace("email:", "");
                    aux = aux.Replace("email", "");
                    aux = aux.Replace("multiple locations", "");
                    aux = aux.Replace("valid at", "");
                    aux = aux.Replace("view locations", "");
                    aux = aux.Replace(" or ", " ");
                    if (aux1 != aux)
                    {
                        RemoveSpaces(ref aux);
                        dd.data[i] = aux;
                    }
                }

                if (dd.data[0] == "http://www.dealfind.com/toronto")
                {
                    if ((dd.data[14] == "") && (dd.data[16] == ""))
                    {
                        if (dd.data[15] != "")
                        {
                            // Contacts are in the wrong place. Moving them from City to Contact
                            if (dd.data[19] == "")
                                dd.data[19] = dd.data[15];
                            else
                                dd.data[19] = dd.data[19] + "; " + dd.data[15];
                            dd.data[15] = "";
                        }
                        if (dd.data[43] != "")
                        {
                            // Contacts are in the wrong place. Moving them from Province to Contact
                            if (dd.data[19] == "")
                                dd.data[19] = dd.data[43];
                            else
                                dd.data[19] = dd.data[19] + ", " + dd.data[43];
                            dd.data[43] = "";
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

//  if Latitude contains both Lat and Longitude data, Longitude field is empty
                if ((dd.data[11] != "") && (dd.data[12] == ""))
                {
                    SeparateLatLong(ref dd.data[11], ref dd.data[12]);
                }

// remove latitude and longitude if it points to nowhere
                if ((dd.data[11].Length>=3) && (dd.data[11].Substring(0,3) == "56.") && (dd.data[12].Length>=5) && (dd.data[12].Substring(0,5) == "-106."))
                {
                    dd.data[11] = "";
                    dd.data[12] = "";
                    if ((dd.data[18] != "") && (dd.data[18].IndexOf("56.") != -1))
                        dd.data[18] = "";
                }
                if ((dd.data[11].Length >= 3) && (dd.data[11].Substring(0, 3) == "51.") && (dd.data[12].Length >= 5) && (dd.data[12].Substring(0, 5) == "-85."))
                {
                    dd.data[11] = "";
                    dd.data[12] = "";
                    if ((dd.data[18] != "") && (dd.data[18].IndexOf("56.") != -1))
                        dd.data[18] = "";
                }

                // If googlemaps link has online, it is invalid, so remove it. Also remove Lat/Long
                if (dd.data[18] != "")
                {
                    string aux = dd.data[18].ToLower();
                    if ((aux.IndexOf("=online+") != -1) || (aux.IndexOf("+online+") != -1))
                    {
                        dd.data[18] = "";
                        dd.data[11] = "";
                        dd.data[12] = "";
                    }
                }

                // Round Latitude to 3 decimals
                if (dd.data[11] != "")
                {
                    string s = dd.data[11].ToString();
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
                    lat = Math.Round(lat, 3);
                    dd.data[11] = lat.ToString();
                }

                // Round Longitude to 3 decimals
                if (dd.data[12] != "")
                {
                    string s = dd.data[12].ToString();
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
                    longit = Math.Round(longit, 3);
                    dd.data[12] = longit.ToString();
                }

                // if there is no googlemaps link, we create the URL
                if (dd.data[18] == "")
                {
                    if ((dd.data[11] != "") && (dd.data[12] != ""))
                    {
                        dd.data[18] = "http://maps.google.com/maps?q=" + dd.data[11] + ", " + dd.data[12];
                    }
                }

                
// end of Data Handling


                SqlCommand myCommandDeal = new SqlCommand("INSERT INTO DealsList (Website, DealID, DealLinkURL, Category, Image, Description, DealerID, RegularPrice, OurPrice, PayOutAmount, PayOutLink, ExpiryTime, MaxNumberVouchers, MinNumberVouchers, PaidVoucherCount, DealExtractedTime, Highlights, BuyDetails, DealText, Reviews) Values (@Website, @DealID, @DealLinkURL, @Category, @Image, @Description, @DealerID, @RegularPrice, @OurPrice, @PayOutAmount, @PayOutLink, @ExpiryTime, @MaxNumberOfVouchers, @MinNumberOfVouchers, @PaidVoucherCount, @DealExtractedTime, @Highlights, @BuyDetails, @DealText, @Reviews)", myConnection);
                SqlCommand myCommandOtherData = new SqlCommand("INSERT INTO OtherData (Website, DealID, ListOfCities, SideDeals, RegularPrice, OurPrice, Saved, Discount, SecondsTotal, SecondsElapsed, RemainingTime, ExpiryTime, DealSoldOut, DealEnded, DealValid, RelatedDeals) Values (@Website, @DealID, @ListOfCities, @SideDeals, @RegularPrice, @OurPrice, @Saved, @Discount, @SecondsTotal, @SecondsElapsed, @RemainingTime, @ExpiryTime, @DealSoldOut, @DealEnded, @DealValid, @RelatedDeals)", myConnection);

                

                SqlParameter p41 = new SqlParameter();
                p41.ParameterName = "@DealerID";
                p41.Value = getDealerID(dd.data, myConnection, writer, ref AtTheEnd);
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
                    p18.Value = dd.data[20];
                myCommandDeal.Parameters.Add(p18);

                SqlParameter p19 = new SqlParameter();
                p19.ParameterName = "@OurPrice";
                if ((dd.data[21] == "") || (dd.data[21] == "{:-("))
                    p19.Value = DBNull.Value;
                else
                    p19.Value = dd.data[21];
                myCommandDeal.Parameters.Add(p19);

                SqlParameter p22 = new SqlParameter();
                p22.ParameterName = "@PayOutAmount";
                if ((dd.data[24] == "") || (dd.data[24] == "{:-("))
                    p22.Value = DBNull.Value;
                else
                   p22.Value = dd.data[24];
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
                    p27.Value = dd.data[29];
                myCommandDeal.Parameters.Add(p27);

                SqlParameter p28 = new SqlParameter();
                p28.ParameterName = "@MaxNumberOfVouchers";
                if ((dd.data[30] == "") || (dd.data[30] == "{:-("))
                    p28.Value = DBNull.Value;
                else
                    p28.Value = dd.data[30];
                myCommandDeal.Parameters.Add(p28);

                SqlParameter p29 = new SqlParameter();
                p29.ParameterName = "@MinNumberOfVouchers";
                if ((dd.data[31] == "") || (dd.data[31] == "{:-("))
                    p29.Value = DBNull.Value;
                else
                    p29.Value = dd.data[31];
                myCommandDeal.Parameters.Add(p29);

                SqlParameter p31 = new SqlParameter();
                p31.ParameterName = "@PaidVoucherCount";
                if ((dd.data[35] == "") || (dd.data[35] == "{:-("))
                    p31.Value = DBNull.Value;
                else
                    p31.Value = dd.data[35];
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
                p42.Value = DateTime.Now.ToString("HH:mm:ss tt");
                myCommandDeal.Parameters.Add(p42);

//                SqlParameter p41 = new SqlParameter();
//                p41.ParameterName = "@DealerID";
//                string DealerID = dd.data[7];
//                if (DealerID.Length > 15)
//                    DealerID = DealerID.Substring(0, 15);
//                p41.Value = DealerID;
//                myCommandDeal.Parameters.Add(p41);

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
                    p18a.Value = dd.data[20];
                myCommandOtherData.Parameters.Add(p18a);

                SqlParameter p19a = new SqlParameter();
                p19a.ParameterName = "@OurPrice";
                if ((dd.data[21] == "") || (dd.data[21] == "{:-("))
                    p19a.Value = DBNull.Value;
                else
                    p19a.Value = dd.data[21];
                myCommandOtherData.Parameters.Add(p19a);

                SqlParameter p20 = new SqlParameter();
                p20.ParameterName = "@Saved";
                if ((dd.data[22] == "") || (dd.data[22] == "{:-("))
                    p20.Value = DBNull.Value;
                else
                    p20.Value = dd.data[22];
                myCommandOtherData.Parameters.Add(p20);

                SqlParameter p21 = new SqlParameter();
                p21.ParameterName = "@Discount";
                if ((dd.data[23] == "") || (dd.data[23] == "{:-("))
                    p21.Value = DBNull.Value;
                else
                    p21.Value = dd.data[23];
                myCommandOtherData.Parameters.Add(p21);

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
                    p27a.Value = dd.data[29];
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
            while (b != -1)
            {
                e = aux.IndexOf(" ", b);
                if (e == -1)
                    e = aux.Length - 1;
                b = aux.LastIndexOf(" ", b);
                if (b == -1)
                    b = 0;
                if ((contact == "") || (contact == "{:-("))
                    contact = aux.Substring(b, e - b + 1);
                else
                    contact = contact + "; " + aux.Substring(b, e - b + 1);
                aux = aux.Replace(aux.Substring(b, e - b + 1), "");
                RemoveSpaces(ref aux);
                b = aux.IndexOf("@");
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
                RemoveSpaces(ref aux);
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
                RemoveSpaces(ref aux);
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
                    RemoveSpaces(ref aux);
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
                    b += 4; // lenght of word call
                    if (aux[b] == 'e')
                        b += 1; // the word was phone and not call
                }
            }
            else b = 0;
            if (b != -1)
            {
                while ((b < aux.Length) && ((aux[b] == ' ') || (aux[b] == ':')))
                    b += 1;
                if ((b < aux.Length) && ((aux[b] == '(') || ((aux[b] >= '0') && (aux[b] <= '9'))))
                {
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
                    dd.data[19] = aux.Substring(b, aux.Length - b);
                    aux = aux.Replace(dd.data[19], "");
                    RemoveSpaces(ref aux);
                }
                else phone = true;
            }
            return aux;
        }

        string getDealerID(string[] p, SqlConnection myConnection2, StreamWriter writer, ref string AtTheEnd)
        {
            Boolean similar;
            short id = 0, bigger = 0;
            int count = 0;
            string temp;
/*            SqlConnection myConnection2 = new SqlConnection("server=MEDIACONNECT-PC\\MCAPPS; Trusted_Connection=yes; database=Deals; connection timeout=15");
            try
            {
                myConnection2.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }*/
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
//                    int binaryCount = 0;
                    temp = myReader["DealerID"].ToString();
                    id = Convert.ToInt16(temp.Substring(15, temp.Length - 15));
                    if (id > bigger)
                        bigger = id;

                    if ((p[8] == myReader["URL"].ToString()) || ((p[8] == "") && (myReader["URL"] == DBNull.Value)))
                    {
                        count += 1;
//                        if ((aux == "") && (p[8] != ""))
//                            binaryCount += 1;
                    }

                    if ((p[11] == myReader["Latitude"].ToString()) || ((p[11] == "") && (myReader["Latitude"] == DBNull.Value)))
                    {
                        count += 1;
//                        if ((aux == "") && (p[11] != ""))
//                            binaryCount += 2;
                    }


                    if ((p[12] == myReader["Longitude"].ToString()) || ((p[12] == "") && (myReader["Longitude"] == DBNull.Value)))
                    {
                        count += 1;
//                        if ((aux == "") && (p[12] != ""))
//                            binaryCount += 4;
                    }

                    if ((p[13] == myReader["FullAddress"].ToString()) || ((p[13] == "") && (myReader["FullAddress"] == DBNull.Value)))
                    {
                        count += 1;
//                        if ((aux == "") && (p[13] != ""))
//                            binaryCount += 8;
                    }

                    if ((p[14] == myReader["StreetName"].ToString()) || ((p[14] == "") && (myReader["StreetName"] == DBNull.Value)))
                    {
                        count += 1;
//                        if ((aux == "") && (p[14] != ""))
//                            binaryCount += 16;
                    }

                    if ((p[15] == myReader["City"].ToString()) || ((p[15] == "") && (myReader["City"] == DBNull.Value)))
                    {
                        count += 1;
//                        if ((aux == "") && (p[15] != ""))
//                            binaryCount += 32;
                    }

                    if ((p[43] == myReader["Province"].ToString()) || ((p[43] == "") && (myReader["Province"] == DBNull.Value)))
                    {
                        count += 1;
                        //                        if ((aux == "") && (p[15] != ""))
                        //                            binaryCount += 32;
                    }

                    if ((p[16] == myReader["PostalCode"].ToString()) || ((p[16] == "") && (myReader["PostalCode"] == DBNull.Value)))
                    {
                        count += 1;
//                        if ((aux == "") && (p[16] != ""))
//                            binaryCount += 64;
                    }

                    if ((p[17] == myReader["Country"].ToString()) || ((p[17] == "") && (myReader["Country"] == DBNull.Value)))
                    {
                        count += 1;
//                        if ((aux == "") && (p[17] != ""))
//                            binaryCount += 128;
                    }

                    if ((p[18] == myReader["Map"].ToString()) || ((p[18] == "") && (myReader["Map"] == DBNull.Value)))
                    {
                        count += 1;
//                        if ((aux == "") && (p[18] != ""))
//                            binaryCount += 256;
                    }

                    if ((p[19] == myReader["Contact"].ToString()) || ((p[19] == "") && (myReader["Contact"] == DBNull.Value)))
                    {
                        count += 1;
//                        if ((aux == "") && (p[19] != ""))
//                            binaryCount += 512;
                    }

                    if (count == 11) 
                    {
                        myReader.Close();
                        return (temp);
                    }

/*         NOT FINISHED: To insert data if identified that the dealer is the same but don't have its complete description
                    int i = binaryCount;

                    //If i == 1023, everything is NULL in the stored dealer, so we can't assume that the stored and the new dealers with only the same name are the same
                    if (i != 1023)
                    {
                        //Everything but the country is NULL in the stored dealer, so we can't assume that the stored and the new dealers in the same country with the same name are the same
                        if (i == 895)
                        {
                            Console.WriteLine("WARNING: Check Dealer " + p[7] + ". Only country is different than NULL. I can't assume that both are the same.\n");
                            AtTheEnd = AtTheEnd + "WARNING: Check Dealer " + p[7] + ". Only country is different than NULL. I can't assume that both are the same.\n";
                        }
                        else
                        {
                            if (i >= 512)
                            {
                                i -= 512;
                            }
                            if (i >= 256)
                            {
                                i -= 256;
                            }
                            if (i >= 128)
                            {
                                i -= 128;
                            }
                            if (i >= 64)
                            {
                                i -= 64;
                            }
                            if (i >= 32)
                            {
                                i -= 32;
                            }
                            if (i >= 16)
                            {
                                i -= 16;
                            }
                            if (i >= 8)
                            {
                                i -= 8;
                            }
                            if (i >= 4)
                            {
                                i -= 4;
                            }
                            if (i >= 2)
                            {
                                i -= 2;
                            }
                            if (i >= 1)
                            {

                            }
                        }
                    }*/

                    count = 0;
                }
                myReader.Close();
                if (similar)
                {
                    Console.WriteLine("WARNING: Check Dealer " + p[7] + ". There are Dealers with the same data (part of)");
                    AtTheEnd = AtTheEnd + "WARNING: Check Dealer " + p[7] + ". There are Dealers with the same data (part of)\n";
                    //                    writer.WriteLine("\n\nWARNING: Check Dealer " + p[7] + ". There are Dealers with the same data (part of)\n\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
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
                RemoveSpaces(ref p_2);
                RemoveSpaces(ref p);
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
            string myChoice; // only used to have a pause at the end of the program
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
            tag.data[8] = "?\"\\\"og:url\\\" content=\\\"\";@\"\\\"\"";
            tag.data[9] = "?\"\\\"og:image\\\" content=\\\"\";@\"\\\"\"";
            tag.data[10] = "?\"\\\"og:description\\\" content=\\\"\";@\"\\\"/>\";||?\"\var DealName = \\\"\";@\"\\\";\"";
            tag.data[11] = "?\"\\\"og:latitude\\\" content=\\\"\";@\"\\\"\"";
            tag.data[12] = "?\"\\\"og:longitude\\\" content=\\\"\";@\"\\\"\"";
            tag.data[13] = "?\"ocations:</strong>\";@\"<div class=\\\"divReviews\\\">\"";
            tag.data[14] = "?\"\\\"og:street-address\\\" content=\\\"\";@\"\\\" />\" ";
            tag.data[15] = "?\"\\\"og:locality\";?\"content=\\\"\"-\"/>\";@\",\"||?\"\\\"og:locality\\\" content=\\\"\";@\"\\\" />\"";
            tag.data[16] = "?\"\\\"og:postal-code\\\" content=\\\"\";@\"\\\" />\"";
            tag.data[17] = "?\"\\\"og:country-name\\\" content=\\\"\";@\"\\\" />\"";
            tag.data[18] = "?\"itemprop=\\\"maps\\\" href=\\\"\";@\"\\\" target=\"";
            tag.data[19] = "";
            tag.data[20] = "?\"var RegularPriceHTML = '\";@\"';\"";
            tag.data[21] = "?\"var OurPriceHTML = '\";@\"';\"";
            tag.data[22] = "?2\"var YouSaveHTML = '\";@\"(\"";
            tag.data[23] = "?2\"var YouSaveHTML = '\";?\"(\";@\")';\"";
            tag.data[24] = "?\"Share and get paid\";@\"per\"";
            tag.data[25] = "";
            tag.data[26] = "?\"var DealSeconds_Total =\";@\";\"";
            tag.data[27] = "?\"var DealSeconds_Elapsed =\";@\";\"";
            tag.data[28] = "";
            tag.data[29] = "";
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
            tag.data[43] = "?\"\\\"og:locality\";?\"content=\\\"\"-\"/>\";?\",\";@\"\\\" />\""; 
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
            tag.data[13] = "?\"Locations:\";@\"<div style=\\\"float:right\\\">\"";
            tag.data[14] = "?\"<div id=\\\"companyAddress\\\">\";@\"</div>\"";
            tag.data[15] = "";
            tag.data[16] = "";
            tag.data[17] = "";
            tag.data[18] = "(?\"http://maps.google.ca/maps?\";?<\"\\\"\";@\"\\\"\")";
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
            tag.data[29] = "";
            tag.data[30] = "";
            tag.data[31] = "?\"more needed<br/>\";?<\"10px\\\">\";@\"more needed<br/>\"";
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
            tag.data[11] = "?\"var sites = [[\";?\",\";@\",\"";
            tag.data[12] = "?\"var sites = [[\";?2\",\";@\",\"";
            tag.data[13] = "?\"<strong>Locations\";?\"</p>\";@\"<strong>Reviews\";||?\"<strong>Locations\";?\"</p>\";@\"<br /><a href=\\\"\";||?\"<!-- End Deal Information -->\";?<\"<strong>Reviews\";?<\"</p>\";@\"<strong>Reviews\"||?\"<!-- End Deal Information -->\";?<\"<br /><a href=\\\"\";?<\"<div>\";?\"</p>\n\";@\"<br /><a href=\\\"\"||?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";?\"</li></ul>\";@\"<br /><a href=\\\"\"||?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";@\"<br /><a href=\\\"\"";
            tag.data[14] = "";
            tag.data[15] = "";
            tag.data[16] = "";
            tag.data[17] = "";
            tag.data[18] = "";
            tag.data[19] = "";
            tag.data[20] = "?\"<td>Regular Price:</td><td>\";@\"</td>\"";
            tag.data[21] = "?\">Buy For\";@\"</a></span>\"";
            tag.data[22] = "?\"<td>You Save:</td><td>\";@\"</td>\"";
            tag.data[23] = "?\"<td>Discount:</td><td>\";@\"</td>\"";
            tag.data[24] = "";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "";
            tag.data[29] = "?\"TargetDate = \\\"\";@\"\\\";\"";
            tag.data[30] = "";
            tag.data[31] = "?\"class=\\\"deal_activated\\\">\";?\" at\";@\"</span>\"";
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
            tag.data[11] = "?\"var sites = [[\";?\",\";@\",\"";
            tag.data[12] = "?\"var sites = [[\";?2\",\";@\",\"";
            tag.data[13] = "?\"<strong>Locations\";?\"</p>\";@\"<strong>Reviews\";||?\"<strong>Locations\";?\"</p>\";@\"<br /><a href=\\\"\";||?\"<!-- End Deal Information -->\";?<\"<strong>Reviews\";?<\"</p>\";@\"<strong>Reviews\"||?\"<!-- End Deal Information -->\";?<\"<br /><a href=\\\"\";?<\"<div>\";?\"</p>\n\";@\"<br /><a href=\\\"\"||?\"<!-- End Deal Information -->\"; ?<\"<br /><a href=\\\"\";?<\"</p>\";?\"</li></ul>\";@\"<br /><a href=\\\"\"";
            tag.data[14] = "";
            tag.data[15] = "";
            tag.data[16] = "";
            tag.data[17] = "";
            tag.data[18] = "";
            tag.data[19] = "";
            tag.data[20] = "?\"<td>Regular Price:</td><td>\";@\"</td>\"";
            tag.data[21] = "?\">Buy For\";@\"</a></span>\"";
            tag.data[22] = "?\"<td>You Save:</td><td>\";@\"</td>\"";
            tag.data[23] = "?\"<td>Discount:</td><td>\";@\"</td>\"";
            tag.data[24] = "";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "";
            tag.data[29] = "?\"TargetDate = \\\"\";@\"\\\";\"";
            tag.data[30] = "";
            tag.data[31] = "?\"class=\\\"deal_activated\\\">\";?\" at\";@\"</span>\"";
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
            tag.data[13] = "(?\"<span class=\\\"street_1\\\">\";@\"<span class=\\\"directions\\\">\")";
            tag.data[14] = "";
            tag.data[15] = "";
            tag.data[16] = "";
            tag.data[17] = "";
            tag.data[18] = "?\"class=\\\"directions\\\"><a href=\\\"\";@\"\\\"\"";
            tag.data[19] = "";
            tag.data[20] = "";
            tag.data[21] = "?\"<div class=\\\"deal-price \\\">\";?\"</sup>\";@\"</div>\"";
            tag.data[22] = "";
            tag.data[23] = "?\"id=\\\"percentage\\\">\";@\"<\"";
            tag.data[24] = "";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "?\"class=\\\"last\\\">\";?\">\";@\"</div>\"";
            tag.data[29] = "";
            tag.data[30] = "";
            tag.data[31] = "";
            tag.data[32] = "";
            tag.data[33] = "";
            tag.data[34] = "?\"class=\\\"deal-buy-box buy-now-over\";@\"\\\">\";||?\"class=\\\"deal-buy-box buy-now-soldout\";@\"\\\">\"";
            tag.data[35] = "?\"class=\\\"purchased\\\">\";?\">\";@\"<\"";
            tag.data[36] = "";
            tag.data[37] = "?\"class=\\\"fine-print\\\">\";?\"<p>\";@\"</p>\"";
            tag.data[38] = "?\"<div id=\\\"sfwt_short_1\\\"><p>\";@\"</p> <a class=\\\"sfwt\\\"\"";
            tag.data[39] = "";
            tag.data[40] = "";
            tag.data[41] = "?2\"/\";@\"-\""; //find DealID/AlternativeID in weblink
            tag.data[42] = ""; //Alternative ID
            tag.data[43] = "";
            ListTags.Add(tag);

            tag = new Tags();
            tag.data[0] = "http://www.dealticker.com/toronto_en_1categ.html";
            tag.data[1] = "?\"no_deal=true\";@\";\" || ?\"404 Not Found\";@\">\"";
            tag.data[2] = "?\"<div id=\\\"city_list\\\"\";(?\"<a href=\\\"http://www.dealticker.com\";@\"\\\"\")-\"<ul class=\\\"lavaLampWithImage\\\" id=\\\"1\\\">\"";
            tag.data[3] = "(?\"<td colspan=\\\"3\\\" valign='top'\";?3\"<a href=\\\"\";?3\"/\";@\"\\\"\")-\"<td valign=\\\"bottom\\\" style=\\\"height: 170px;\\\">\"";
            tag.data[4] = "?\"http://www.dealticker.com/product.php/tab/1/product_id/\";@\"\\\"\"";
            tag.data[5] = "?\"<div class=\\\"short_description\\\"><a href=\\\"\"@\"\\\"\"";
            tag.data[6] = "";
            tag.data[7] = "?\"class=\\\"fine_print\\\">Location\";?\"<tr><td><b>\";@\"</b>\"||?\"class=\\\"fine_print\\\">Location\";?\"<b>\";@\"</b>\"";
            tag.data[8] = "?\"<div class=\\\"fine_print\\\">Location\";?\"<a href=\\\"http:\"-\"</div></div>\";?<\"\\\"\";@\"\\\"\"";
            tag.data[9] = "?\"<img id='product_img' src='\";@\"'\"";
            tag.data[10] = "?\"name=\\\"description\\\" content=\\\"\";@\"\\\"\"";
            tag.data[11] = "?\"google.maps.LatLng(\";@\",\"";
            tag.data[12] = "?\"google.maps.LatLng(\";?\",\";@\")\"";
            tag.data[13] = "?\"<div id=\\\"location_description\\\"><p>\";@\"<\";||?\"class=\\\"fine_print\\\">Location\";(?\"</b><br>\";@\"</div>\")||?\"class=\\\"fine_print\\\">Location\";(?\"</b></td></tr><tr><td>\";@\"</div>\")";
            tag.data[14] = "?\"class=\\\"fine_print\\\">Location\";?2\"</td></tr><tr><td>\";@\"</\"||?\"class=\\\"fine_print\\\">Location\";?\"</b><br>\";?\",\"-\"</div>\";@\",\"";
            tag.data[15] = "?\"class=\\\"fine_print\\\">Location\";?\"</b></td></tr><tr><td>\";@\"<\"||?\"class=\\\"fine_print\\\">Location\";?\"</b><br>\";@\",\"";
            tag.data[16] = "?\"class=\\\"fine_print\\\">Location\";?3\"</td></tr><tr><td>\";@\"</\"||?\"class=\\\"fine_print\\\">Location\";?\"</b><br>\";?2\",\"-\"</div>\";@\",\"";
            tag.data[17] = "";
            tag.data[18] = "";
            tag.data[19] = "?\"class=\\\"fine_print\\\">Location\";?4\"</td></tr><tr><td>\";@\"</\"||?\"class=\\\"fine_print\\\">Location\";?\"</b><br>\";?3\",\"-\"</div>\";@\"</div>\"";
            tag.data[20] = "?\"class=\\\"value\\\">$\";@\"<\"";
            tag.data[21] = "?\"class=\\\"price\\\"\";?\"$\";@\"<\"";
            tag.data[22] = "?2\"class=\\\"value\\\">$\";@\"<\"";
            tag.data[23] = "?2\"class=\\\"value\\\">\";@\"%<\"";
            tag.data[24] = "?\"Share Today's Deal and get $\";@\" \"";
            tag.data[25] = "";
            tag.data[26] = "";
            tag.data[27] = "";
            tag.data[28] = "?\"Time Remaining:\";?2\"<tr>\";@\"</tr>\"";
            tag.data[29] = "";
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
            tag.data[40] = ""; //find DealID/AlternativeID in weblink
            tag.data[42] = ""; //Alternative ID
            tag.data[43] = "";
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



            for (int i = 0; i < ListTags.Count; i++)
            {
      //          int i = 1;
                string website = ListTags.ElementAt(i).data[0];
                Extraction site = new Extraction(ListTags.ElementAt(i), baseaddress.ElementAt(i));
//                string website = ListTags.ElementAt(i).data[0];
//                CityExtraction site = new CityExtraction(ListTags.ElementAt(i));
                Thread t = new Thread(new ThreadStart(site.ExtractingCities));
                t.Name=website;
//                CityThreads.Add(t);
                t.Start();
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

            myChoice = Console.ReadLine();
        }
    }
}
