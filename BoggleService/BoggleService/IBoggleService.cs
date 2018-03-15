using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Boggle
{
    [ServiceContract]
    public interface IBoggleService
    {
        /// <summary>
        /// Sends back index.html as the response body.
        /// </summary>
        [WebGet(UriTemplate = "/api")]
        Stream API();

        /// <summary>
        /// Returns the nth word from dictionary.txt.  If there is
        /// no nth word, responds with code 403.
        /// </summary>
        [WebGet(UriTemplate = "/word?index={n}")]
        string WordAtIndex(int n);

        /// <summary>
        /// Returns m consecutive words from dictonary.txt, beginning
        /// with the nth word.  If this is not possible, responds with 
        /// code 403.
        /// </summary>
        [WebGet(UriTemplate = "/words?index={n}&count={m}")]
        IList<string> WordsAtIndex(int n, int m);

        // Given a list of objects, where each object has a Text property
        // and a Value property, returns the object that (1) has a Text
        // value that appears in the dictionary and (2) has the highest
        // Value property among all pairs that pass test (1),
        [WebInvoke(Method = "PUT", UriTemplate = "/max")]
        TextValuePair MaxPair(List<TextValuePair> pairs);
    }
}
