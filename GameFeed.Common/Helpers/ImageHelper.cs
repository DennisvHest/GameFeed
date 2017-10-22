namespace GameFeed.Common.Helpers {

    public static class ImageHelper {

        public static string GetFullSizedImageUrl(string url) {
            return url.Replace("/t_thumb", "");
        }
    }
}
