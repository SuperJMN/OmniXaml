namespace OmniXaml.Wpf.Tests
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Xunit;
    using Resources = Xaml.Tests.Resources.Wpf;

    public class LoadingTests : GivenAXamlXmlLoader
    {
        [StaFact]
        public void Window()
        {
            var windowType = typeof(Window);

            var actualInstance = LoadXaml(Resources.Window);
            Assert.IsType(windowType, actualInstance);
        }

        [StaFact]
        public void WindowWithContent()
        {
            var windowType = typeof(Window);
            var textBlockType = typeof(TextBlock);

            var actualInstance = LoadXaml(Resources.WindowWithContent);
            Assert.IsType(windowType, actualInstance);
            var window = (Window)actualInstance;
            Assert.IsType(textBlockType, window.Content);
            var textBlock = (TextBlock)window.Content;
            Assert.Equal("Saludos cordiales!", textBlock.Text);
        }

        [StaFact]
        public void DataTemplate()
        {
            var visualTree = LoadXaml(Resources.DataTemplate);
        }

        [StaFact]
        public void ShowCase()
        {
            var visualTree = LoadXaml(Resources.ShowCase);
        }

        [StaFact]
        public void MicroShowCase()
        {
            var visualTree = LoadXaml(Resources.MicroShowCase);
        }

        [StaFact]
        public void Stage1()
        {
            var visualTree = LoadXaml(Resources.Stage1);
        }

        [StaFact]
        public void Stage2()
        {
            var visualTree = LoadXaml(Resources.Stage2);
        }

        [StaFact]
        public void Stage3()
        {
            var visualTree = LoadXaml(Resources.Stage3);
        }

        [StaFact]
        public void ColorResource()
        {
            var visualTree = (Window)LoadXaml(Resources.ColorResource);
            var color = (Color)visualTree.FindResource("color");
            Assert.Equal(0xff, color.A);
            Assert.Equal(0x80, color.R);
            Assert.Equal(0x80, color.G);
            Assert.Equal(0x80, color.B);
        }

        [StaFact]
        public void SolidColorBrushResource()
        {
            var visualTree = (Window)LoadXaml(Resources.SolidColorBrushResource);
            var brush = (SolidColorBrush)visualTree.FindResource("brush");
            var color = brush.Color;
            Assert.Equal(0xff, color.A);
            Assert.Equal(0x80, color.R);
            Assert.Equal(0x80, color.G);
            Assert.Equal(0x80, color.B);
        }

        private class EventHandlingWindow : Window
        {
            public bool EventFired { get; private set; } = false;
            private void TextChanged(object sender, RoutedEventArgs args)
            {
                EventFired = true;
            }
        }

        [StaFact]
        public void WindowWithEventHandler()
        {
            var visualTree = (EventHandlingWindow)LoadXaml(Resources.WindowWithEventHandler, new EventHandlingWindow());
            var textBox = (TextBox)visualTree.Content;
            textBox.Text = "New Text";
            Assert.True(visualTree.EventFired);
        }
    }
}
