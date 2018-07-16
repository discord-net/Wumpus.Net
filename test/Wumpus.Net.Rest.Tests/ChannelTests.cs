using Voltaic;
using Wumpus.Entities;
using Wumpus.Requests;
using Xunit;

namespace Wumpus.Rest.Tests
{
    public class ChannelTests : BaseTest
    {
        [Fact]
        public void GetChannelAsync()
        {
            RunTest(c => c.GetChannelAsync(123), x =>
            {
                Assert.Equal(123UL, x.Id.RawValue);
            });
        }
        [Fact]
        public void ReplaceGuildChannelAsync()
        {
            RunTest(c => c.ReplaceGuildChannelAsync(123, new ModifyGuildChannelParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }
        [Fact]
        public void ReplaceTextChannelAsync()
        {
            RunTest(c => c.ReplaceTextChannelAsync(123, new ModifyTextChannelParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }
        [Fact]
        public void ReplaceVoiceChannelAsync()
        {
            RunTest(c => c.ReplaceVoiceChannelAsync(123, new ModifyVoiceChannelParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }
        [Fact]
        public void ModifyGuildChannelAsync()
        {
            RunTest(c => c.ModifyGuildChannelAsync(123, new ModifyGuildChannelParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }
        [Fact]
        public void ModifyTextChannelAsync()
        {
            RunTest(c => c.ModifyTextChannelAsync(123, new ModifyTextChannelParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }
        [Fact]
        public void ModifyVoiceChannelAsync()
        {
            RunTest(c => c.ModifyVoiceChannelAsync(123, new ModifyVoiceChannelParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }
        [Fact]
        public void DeleteChannelAsync()
        {
            RunTest(c => c.DeleteChannelAsync(123), x =>
            {
                Assert.Equal(123UL, x.Id.RawValue);
            });
        }

        [Fact]
        public void GetChannelMessagesAsync()
        {
            RunTest(c => c.GetChannelMessagesAsync(123, new GetChannelMessagesParams
            {
                // TODO: Impl
            }), x =>
            {
                foreach (var msg in x)
                    Assert.Equal(123UL, msg.ChannelId.RawValue);
                // TODO: Impl
            });
        }
        [Fact]
        public void GetChannelMessageAsync()
        {
            RunTest(c => c.GetChannelMessageAsync(123, 456), x =>
            {
                Assert.Equal(123UL, x.ChannelId.RawValue);
                Assert.Equal(456UL, x.Id.RawValue);
            });
        }
        [Fact]
        public void CreateMessageAsync()
        {
            RunTest(c => c.CreateMessageAsync(123, new Requests.CreateMessageParams
            {
                Content = (Utf8String)"test",
                Embed = new Embed { Author = new EmbedAuthor { Name = (Utf8String)"testtest" }, Url = (Utf8String)"http://discordapp.com" },
                IsTextToSpeech = true
            }), x =>
            {
                Assert.Equal(123UL, x.ChannelId.RawValue);
                Assert.Equal((Utf8String)"test", x.Content);
                Assert.Contains(new Embed { Author = new EmbedAuthor { Name = (Utf8String)"testtest" }, Url = (Utf8String)"http://discordapp.com" }, x.Embeds);
                Assert.True(x.IsTextToSpeech);
            });
        }
        [Fact]
        public void ModifyMessageAsync()
        {
            RunTest(c => c.ModifyMessageAsync(123, 456, new ModifyMessageParams
            {
                // TODO: Impl
            }), x =>
            {
                Assert.Equal(123UL, x.ChannelId.RawValue);
                Assert.Equal(456UL, x.Id.RawValue);
                // TODO: Impl
            });
        }
        [Fact]
        public void DeleteMessageAsync()
        {
            RunTest(c => c.DeleteMessageAsync(123, 456));
        }
        [Fact]
        public void DeleteMessagesAsync()
        {
            RunTest(c => c.DeleteMessagesAsync(123, new DeleteMessagesParams(new Snowflake[] { 456UL, 789UL })));
        }

        [Fact]
        public void GetReactionsAsync()
        {
            RunTest(c => c.GetReactionsAsync(123, 456, (Utf8String)"👌"));
        }
        [Fact]
        public void CreateReactionAsync()
        {
            RunTest(c => c.CreateReactionAsync(123, 456, (Utf8String)"👌"));
        }
        [Fact]
        public void DeleteReactionAsync()
        {
            RunTest(c => c.DeleteReactionAsync(123, 456, (Utf8String)"👌"));
            RunTest(c => c.DeleteReactionAsync(123, 456, 789, (Utf8String)"👌"));
        }
        [Fact]
        public void DeleteAllReactionsAsync()
        {
            RunTest(c => c.DeleteAllReactionsAsync(123, 456));
        }

        [Fact]
        public void EditChannelPermissionsAsync()
        {
            RunTest(c => c.EditChannelPermissionsAsync(123, 456, new ModifyChannelPermissionsParams(PermissionTarget.User, ChannelPermissions.Connect, ChannelPermissions.None)));
        }
        [Fact]
        public void DeleteChannelPermissionsAsync()
        {
            RunTest(c => c.DeleteChannelPermissionsAsync(123, 456));
        }

        [Fact]
        public void GetChannelInvitesAsync()
        {
            RunTest(c => c.GetChannelInvitesAsync(123), x =>
            {
                foreach (var invite in x)
                    Assert.Equal(123UL, invite.Channel.Id.RawValue);
            });
        }
        [Fact]
        public void CreateChannelInviteAsync()
        {
            RunTest(c => c.CreateChannelInviteAsync(123, new CreateChannelInviteParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }

        [Fact]
        public void GetPinnedMessagesAsync()
        {
            RunTest(c => c.GetPinnedMessagesAsync(123), x =>
            {
                foreach (var msg in x)
                    Assert.Equal(123UL, msg.ChannelId.RawValue);
            });
        }
        [Fact]
        public void PinMessageAsync()
        {
            RunTest(c => c.PinMessageAsync(123, 456));
        }
        [Fact]
        public void UnpinMessageAsync()
        {
            RunTest(c => c.UnpinMessageAsync(123, 456));
        }

        [Fact]
        public void AddRecipientAsync()
        {
            RunTest(c => c.AddRecipientAsync(123, 789, new AddChannelRecipientParams((Utf8String)"access_token")));
        }
        [Fact]
        public void RemoveRecipientAsync()
        {
            RunTest(c => c.RemoveRecipientAsync(123, 789));
        }

        [Fact]
        public void TriggerTypingIndicatorAsync()
        {
            RunTest(c => c.TriggerTypingIndicatorAsync(123));
        }
    }
}
