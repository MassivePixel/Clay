using System.Collections.Generic;
using Xamarin.Forms;
using Xunit;
using static MassivePixel.Clay.VirtualDOM;
using Props = System.Collections.Generic.Dictionary<string, object>;

namespace MassivePixel.Clay.Tests
{
    public class TestNodeCreation
    {
        [Fact]
        public void TestCreateButton()
        {
            var s = "Button";
            Assert.Equal(s, h(s).Name);
        }

        [Fact]
        public void TestCreateGenericButton()
        {
            Assert.Equal(nameof(Button), h<Button>().Name);
        }

        [Fact]
        public void TestChildren()
        {
            var root = h("StackLayout", null,
              h("Button"),
              h("Button")
             );

            var dom = Render(root) as StackLayout;

            Assert.NotNull(dom);
            Assert.Equal(2, dom.Children.Count);
            Assert.IsType<Button>(dom.Children[0]);
            Assert.IsType<Button>(dom.Children[1]);
        }

        [Fact]
        public void TestAttribute()
        {
            var text = "Hello world";
            var dom = Render(h("Button", new Dictionary<string, object>
            {
                {"Text", text}
            })) as Button;

            Assert.NotNull(dom);
            Assert.Equal(text, dom.Text);
        }

        [Fact]
        public void TestInvalidAttribute()
        {
            var node = h<Button>(new Dictionary<string, object>
            {
                { "TextColor", 0}
            });
            var root = Render(node) as Button;
            Assert.Equal(default(Color), root.TextColor);
        }

        [Fact]
        public void TestAnonymousAttributes()
        {
            var node = h(nameof(Button), new
            {
                Text = "Hello world"
            });

            Assert.Equal(1, node.Properties.Count);
            Assert.True(node.Properties.ContainsKey("Text"));
            Assert.Equal("Hello world", node.Properties["Text"]);
        }

        [Fact]
        public void TestAttachedProperty()
        {
            var node = h<Button>(new Dictionary<string, object>
            {
                {"Grid.Row", 1}
            });
            var btn = Render(node) as Button;
            Assert.Equal(1, Grid.GetRow(btn));
        }

        [Fact]
        public void TestNodeFromObject()
        {
            var node = h(new Button
            {
                Text = "Hello world",
                TextColor = Color.Red
            });

            Assert.Contains("Text", node.Properties.Keys);
            Assert.Contains("TextColor", node.Properties.Keys);
        }

        [Fact]
        public void TestContentFromObject()
        {
            var node = h(new ContentView
            {
                Content = new Button()
            });
            Assert.True(node.Children.Count > 0);
        }

        [Fact]
        public void TestChildrenFromObject()
        {
            var node = h(new StackLayout
            {
                Children =
                {
                    new Button()
                }
            });
            Assert.True(node.Children.Count > 0);
        }

        [Fact]
        public void TestPropsAlias()
        {
            var node = h("Label", new Props
            {
                {"Text", "Hello world"}
            });
            Assert.Equal(1, node.Properties.Count);
        }
    }
}
