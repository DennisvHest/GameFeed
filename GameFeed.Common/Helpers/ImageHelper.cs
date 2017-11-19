using System.Collections.Generic;
using GameFeed.Common.Enums;

namespace GameFeed.Common.Helpers {

    public static class ImageHelper {

        private static readonly Dictionary<ImageSize, string> ImageSizes = new Dictionary<ImageSize, string> {
            { ImageSize.CoverSmall, "cover_small" },
            { ImageSize.ScreenshotMedium, "screenshot_med" },
            { ImageSize.CoverBig, "cover_big" },
            { ImageSize.LogoMedium, "logo_med" },
            { ImageSize.ScreenshotBig, "screenshot_big" },
            { ImageSize.ScreenshotHuge, "screenshot_huge" },
            { ImageSize.Thumb, "thumb" },
            { ImageSize.Micro, "micro" },
            { ImageSize.Hd, "720p" },
            { ImageSize.FullHd, "1080p" },
        };

        /// <summary>
        /// Returns the URL for retrieving the image with the given ID and size. Default size = FullHD (1080p).
        /// </summary>
        /// <param name="id">ID of the image.</param>
        /// <param name="size">Size of the image. (https://igdb.github.io/api/references/images/)</param>
        /// <returns>The full URL to directing to the image.</returns>
        public static string GetImageUrl(string id, ImageSize size = ImageSize.FullHd) {
            return Settings.ApiImageBaseUrl + $"{ImageSizes[size]}/{id}.jpg";
        }
    }
}
