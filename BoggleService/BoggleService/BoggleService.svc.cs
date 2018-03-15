using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.ServiceModel.Web;
using static System.Net.HttpStatusCode;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
        /// <summary>
        /// The most recent call to SetStatus determines the response code used when
        /// an http response is sent.
        /// </summary>
        /// <param name="status"></param>
        private static void SetStatus(HttpStatusCode status)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = status;
        }

        /// <summary>
        /// Returns a Stream version of index.html.
        /// </summary>
        /// <returns></returns>
        public Stream API()
        {
            SetStatus(OK);
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            return File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "index.html");
        }

        /// <summary>
        /// Demo.  You can delete this.
        /// </summary>
        public string WordAtIndex(int n)
        {
            if (n < 0)
            {
                SetStatus(Forbidden);
                return null;
            }

            string line;
            using (StreamReader file = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "dictionary.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (n == 0) break;
                    n--;
                }
            }

            if (n == 0)
            {
                SetStatus(OK);
                return line;
            }
            else
            {
                SetStatus(Forbidden);
                return null;
            }
        }

        public IList<string> WordsAtIndex(int n, int m)
        {
            if (n < 0 || m < 0)
            {
                SetStatus(Forbidden);
                return null;
            }

            List<string> words = new List<string>();

            try
            {
                using (StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "dictionary.txt"))
                {
                    for (int i = 0; i < n; i++)
                    {
                        string line = file.ReadLine();
                        if (line == null) throw new Exception();
                    }

                    for (int i = 0; i < m; i++)
                    {
                        string line = file.ReadLine();
                        if (line == null) throw new Exception();
                        words.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                SetStatus(Forbidden);
                return null;
            }

            SetStatus(OK);
            return words;
        }


        public TextValuePair MaxPair (List<TextValuePair> pairs)
        {
            HashSet<string> words = new HashSet<string>();
            using (StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "dictionary.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    words.Add(line);
                }

                TextValuePair bestPair = null;
                foreach (TextValuePair pair in pairs)
                {
                    if (words.Contains(pair.Text.ToUpper()))
                    {
                        if (bestPair == null || pair.Value > bestPair.Value)
                        {
                            bestPair = pair;
                        }
                    }
                }

                SetStatus(OK);
                return bestPair;
            }
        }
    }
}

