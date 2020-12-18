﻿using System;
using System.IO;
using System.Net;
using SkiaSharp;
using Xamarin.Forms;
using System.Diagnostics;

namespace Axvr.Xamarin.Markdown.Extensions
{
    /// <summary>
    /// Image extension methods used in <see cref="MdView"/>.
    /// </summary>
    public static class ImageExtensions
    {
        /// <summary>
        /// Render SVG images from a URI.
        /// </summary>
        /// <remarks>
        /// This uses SkiaSharp as the SVG renderer, however SkiaSharp is
        /// fairly limited in terms of the complexity of the SVGs it is able to
        /// render.
        /// </remarks>
        public static void RenderSvg(this Image view, string uri)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(uri);

                var svg = new SkiaSharp.Extended.Svg.SKSvg();
                req.BeginGetResponse((ar) => 
                {
                    var res = (ar.AsyncState as HttpWebRequest).EndGetResponse(ar) as HttpWebResponse;

                    using (var stream = res.GetResponseStream())
                    {
                        if (stream != null)
                        {
                            var picture = svg.Load(stream);

                            using (var image = SKImage.FromPicture(picture, picture.CullRect.Size.ToSizeI()))
                            using (var data = image.Encode(SKEncodedImageFormat.Jpeg, 80))
                            {
                                var ms = new MemoryStream();

                                if (data != null && !data.IsEmpty)
                                {
                                    data.SaveTo(ms);
                                    ms.Seek(0, SeekOrigin.Begin);
                                    ms.Position = 0;
                                    view.Source = ImageSource.FromStream(() => ms);
                                }
                            }
                        }
                    }
                }, req);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to render svg: {ex}");
            }
        }
    }
}
