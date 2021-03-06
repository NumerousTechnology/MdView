﻿using Xamarin.Forms;

namespace MdView.Templates
{
    /// <summary>
    /// Markdown "separator" template view.
    /// </summary>
    ///
    /// <remarks>
    /// Intended for use as <see cref="MdView.SeparatorTemplate"/>.
    /// </remarks>
    public class Separator : BoxView
    {
        /// <summary>
        /// Builds a new default <see cref="Separator"/> instance.
        /// </summary>
        public Separator() : base()
        {
            HeightRequest = 2;
            BackgroundColor = Color.FromHex("#eaecef");
        }
    }
}
