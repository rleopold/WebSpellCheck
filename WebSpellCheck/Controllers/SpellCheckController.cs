using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebSpellCheck.Core;

namespace WebSpellCheck.Controllers
{
    public class SpellCheckController : ApiController
    {
        public IHttpActionResult Post([FromBody]string text)
        {
            var spellChecker = new WebSpellChecker();
            //hashset gives us a performance advantage handling dupes
            var misspelled = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string[] separators = { ",", ".", "!", "?", ";", ":", " ", "\n" };
            var words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries).Distinct(); // don't need to check repeated words or empties
            misspelled.UnionWith(words.Where(spellChecker.IsMisspelled));

            return Json(misspelled.ToArray());
        }
    }
}