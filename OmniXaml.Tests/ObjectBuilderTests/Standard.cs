namespace OmniXaml.Tests.ObjectBuilderTests
{
    using System.Collections;
    using System.Collections.Generic;
    using Model;
    using Xunit;

    public class Standard : ObjectBuilderTestsBase
    {
        private static void AssertAttachedPropertyCollection(IEnumerable<ConstructionNode> nodes)
        {
            var cn = new ConstructionNode(typeof(Window))
            {
                Assignments = new[]
                {
                    new MemberAssignment
                    {
                        Children = nodes,
                        Member = Member.FromAttached<VisualStateManager>("VisualStateGroups")
                    }
                }
            };

            var actual = Create(cn).Result;

            var visualStateGroups = (IEnumerable) VisualStateManager.GetVisualStateGroups(actual);
            Assert.NotEmpty(visualStateGroups);
        }

        [Fact]
        private void AttachedPropertThatIsCollectionMultipleElements()
        {
            AssertAttachedPropertyCollection(new[]
            {
                new ConstructionNode(typeof(VisualStateGroup)),
                new ConstructionNode(typeof(VisualStateGroup)),
                new ConstructionNode(typeof(VisualStateGroup))
            });
        }

        [Fact]
        private void AttachedPropertThatIsCollectionOneElement()
        {
            AssertAttachedPropertyCollection(new[]
            {
                new ConstructionNode(typeof(VisualStateGroup))
            });
        }

        [Fact]
        public void BasicProperty()
        {
            var node = new ConstructionNode(typeof(Window))
            {
                Assignments = new[]
                {
                    new MemberAssignment
                    {
                        Member = Member.FromStandard<Window>(tb => tb.Height),
                        SourceValue = "12"
                    }
                }
            };

            var creationFixture = Create(node);
            Assert.Equal(new Window {Height = 12}, creationFixture.Result);
        }

        [Fact]
        public void Collection()
        {
            var tree = new ConstructionNode(typeof(Collection))
            {
                Assignments = new[]
                {
                    new MemberAssignment
                    {
                        SourceValue = "My title",
                        Member = Member.FromStandard<Collection>(collection => collection.Title)
                    }
                },
                Children = new[]
                {
                    new ConstructionNode(typeof(TextBlock)),
                    new ConstructionNode(typeof(TextBlock))
                }
            };

            var actual = Create(tree).Result;

            var expected = new Collection {new TextBlock(), new TextBlock()};
            expected.Title = "My title";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CollectionProperty()
        {
            var items = new[]
            {
                new ConstructionNode(typeof(TextBlock)),
                new ConstructionNode(typeof(TextBlock)),
                new ConstructionNode(typeof(TextBlock))
            };

            var node = new ConstructionNode(typeof(ItemsControl))
            {
                Assignments = new[]
                {
                    new MemberAssignment
                    {
                        Member = Member.FromStandard<ItemsControl>(tb => tb.Items),
                        Children = items
                    }
                }
            };

            var result = (ItemsControl) Create(node).Result;
            Assert.NotNull(result.Items);
            Assert.IsAssignableFrom<IEnumerable>(result.Items);
            Assert.NotEmpty(result.Items);
        }

        [Fact]
        public void EnumProperty()
        {
            var node = new ConstructionNode(typeof(TextBlock))
            {
                Assignments = new[]
                {
                    new MemberAssignment
                    {
                        Member = Member.FromStandard<TextBlock>(tb => tb.TextWrapping),
                        SourceValue = "NoWrap"
                    }
                }
            };

            var creationFixture = Create(node);
            Assert.Equal(new TextBlock {TextWrapping = TextWrapping.NoWrap}, creationFixture.Result);
        }

        [Fact]
        public void GivenExtensionThatProvidesCollection_TheCollectionIsProvided()
        {
            var extensionNode = new ConstructionNode(typeof(CollectionExtension));

            var node = new ConstructionNode(typeof(ItemsControl))
            {
                Assignments = new[]
                {
                    new MemberAssignment
                    {
                        Member = Member.FromStandard<ItemsControl>(tb => tb.Items),
                        Children = new[] {extensionNode}
                    }
                }
            };

            var creationFixture = Create(node);
            var result = (ItemsControl) creationFixture.Result;
            Assert.NotNull(result.Items);
            Assert.IsAssignableFrom<IEnumerable>(result.Items);
        }
        
        [Fact]
        public void GivenSimpleExtensionThatProvidesAString_TheStringIsProvided()
        {
            var constructionNode = new ConstructionNode(typeof(SimpleExtension))
            {
                Assignments =
                    new List<MemberAssignment>
                    {
                        new MemberAssignment
                        {
                            Member = Member.FromStandard<SimpleExtension>(extension => extension.Property),
                            SourceValue = "MyText"
                        }
                    }
            };

            var node = new ConstructionNode(typeof(TextBlock))
            {
                Assignments = new[]
                {
                    new MemberAssignment
                    {
                        Member = Member.FromStandard<TextBlock>(tb => tb.Text),
                        Children = new[] {constructionNode}
                    }
                }
            };

            var b = Create(node);

            Assert.Equal(new TextBlock {Text = "MyText"}, b.Result);
        }

        [Fact]
        public void LoadInstanceSameType()
        {
            var node = new ConstructionNode(typeof(Window))
            {
                Assignments = new[]
                {
                    new MemberAssignment
                    {
                        Member = Member.FromStandard<Window>(tb => tb.Title),
                        SourceValue = "My title"
                    }
                }
            };

            var expected = new Window {Content = "My content"};
            var fixture = Create(node, expected);

            Assert.True(ReferenceEquals(expected, fixture.Result));
            Assert.Equal(new Window {Content = "My content", Title = "My title"}, fixture.Result);
        }

        [Fact]
        public void ChildrenAssociatedBeforeInflated()
        {
            var tree = new ConstructionNode<ContentControl>
            {
                Assignments = new[]
                {
                    new MemberAssignment
                    {
                        Member = Member.FromStandard<ContentControl>(ctrl => ctrl.Content),
                        Children = new[]
                        {
                            new ConstructionNode<ModelObject>
                            {
                                Assignments = new[]
                                {
                                    new MemberAssignment
                                    {
                                        SourceValue = "TestName",
                                        Member = Member.FromStandard<ModelObject>(obj => obj.Name)
                                    }
                                }
                            }
                        }
                    }
                }
            };
            var actual = (ContentControl)Create(tree).Result;
            
            Assert.True(actual.ContentAssociatedBeforeAssignment);
        }
    }
}