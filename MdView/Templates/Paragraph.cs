﻿using Xamarin.Forms;

namespace MdView.Templates
{
    /// <summary>
    /// The <c>BindingContext</c> object passed to <see cref="MdView.ParagraphTemplate"/> on construction.
    /// </summary>
    public class ParagraphData
    {
        /// <summary>
        /// Unformatted paragraph text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Formatted paragraph text.
        /// </summary>
        public FormattedString FormattedText { get; set; }
    }


    /// <summary>
    /// Markdown "paragraph" template view.
    /// </summary>
    ///
    /// <remarks>
    /// Intended for use as <see cref="MdView.ParagraphTemplate"/>.
    ///
    /// The control will be passed required data as a <see cref="ParagraphData"/>
    /// object set as the <c>BindingContext</c> of the object; firing the
    /// <see cref="OnBindingContextChanged"/> event handler, which renders the Markdown.
    /// </remarks>
    public class Paragraph : Label
    {
        /// <inheritdoc cref="Label.OnBindingContextChanged"/>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is ParagraphData node)
            {
                if (node.FormattedText == null)
                {
                    Text = node.Text;
                }
                else
                {
                    // NOTE: fixes https://github.com/xamarin/Xamarin.Forms/issues/6734
                    foreach (var span in node.FormattedText.Spans)
                    {
                        span.FontSize = FontSize;
                        span.FontAttributes = span.FontAttributes | FontAttributes;
                        span.TextDecorations = span.TextDecorations | TextDecorations;
                        span.CharacterSpacing = CharacterSpacing;
                        span.LineHeight = LineHeight;
                        span.Parent = this;
                    }

                    FormattedText = node.FormattedText;
                }
            }
        }
    }
}
