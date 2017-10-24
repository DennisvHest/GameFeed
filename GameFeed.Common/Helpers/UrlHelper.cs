namespace GameFeed.Common.Helpers {

    public static class UrlHelper {

        public static string PrettifyGameUrl(int gameId, string gameName) {
            //Remove accents
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(gameName);
            gameName =  System.Text.Encoding.ASCII.GetString(bytes);
            gameName = System.Text.RegularExpressions.Regex.Replace(gameName, @"[^A-Za-z0-9\s-]", ""); //Remove all non valid characters          
            gameName = System.Text.RegularExpressions.Regex.Replace(gameName, @"\s+", " ").Trim(); //Convert multiple spaces into one space  
            gameName = System.Text.RegularExpressions.Regex.Replace(gameName, @"\s", "-"); //Replace spaces by dashes
            return $"/game/{gameId}/{gameName}";
        }
    }
}
