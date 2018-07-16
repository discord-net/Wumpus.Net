using Voltaic;
using Wumpus.Entities;
using Wumpus.Requests;
using Xunit;

namespace Wumpus.Rest.Tests
{
    public class GuildTests : BaseTest
    {
        [Fact]
        public void GetGuildAsync()
        {
            RunTest(c => c.GetGuildAsync(123), x =>
            {
                Assert.Equal(123UL, x.Id.RawValue);
            });
        }
        [Fact]
        public void CreateGuildAsync()
        {
            RunTest(c => c.CreateGuildAsync(new CreateGuildParams((Utf8String)"guild_name", (Utf8String)"voice_region")
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }
        [Fact]
        public void ModifyGuildAsync()
        {
            RunTest(c => c.ModifyGuildAsync(123, new ModifyGuildParams
            {
                // TODO: Impl
            }), x =>
            {
                Assert.Equal(123UL, x.Id.RawValue);
                // TODO: Impl
            });
        }
        [Fact]
        public void DeleteGuildAsync()
        {
            RunTest(c => c.DeleteGuildAsync(123));
        }

        [Fact]
        public void GetGuildChannelsAsync()
        {
            RunTest(c => c.GetGuildChannelsAsync(123), x =>
            {
                foreach (var channel in x)
                    Assert.Equal(123UL, channel.GuildId.Value.RawValue);
            });
        }
        [Fact]
        public void CreateCategoryChannelAsync()
        {
            RunTest(c => c.CreateCategoryChannelAsync(123, new CreateGuildChannelParams((Utf8String)"category_channel", ChannelType.Category)
            {
                // TODO: Impl
            }), x =>
            {
                Assert.Equal(123UL, x.GuildId.Value.RawValue);
                Assert.Equal((Utf8String)"category_channel", x.Name);
                Assert.Equal(ChannelType.Category, x.Type);
                // TODO: Impl
            });
        }
        [Fact]
        public void CreateTextChannelAsync()
        {
            RunTest(c => c.CreateTextChannelAsync(123, new CreateTextChannelParams((Utf8String)"text_channel")
            {
                // TODO: Impl
            }), x =>
            {
                Assert.Equal(123UL, x.GuildId.Value.RawValue);
                Assert.Equal((Utf8String)"text_channel", x.Name);
                Assert.Equal(ChannelType.Text, x.Type);
                // TODO: Impl
            });
        }
        [Fact]
        public void CreateVoiceChannelAsync()
        {
            RunTest(c => c.CreateVoiceChannelAsync(123, new CreateVoiceChannelParams((Utf8String)"voice_channel")
            {
                // TODO: Impl
            }), x =>
            {
                Assert.Equal(123UL, x.GuildId.Value.RawValue);
                Assert.Equal((Utf8String)"voice_channel", x.Name);
                Assert.Equal(ChannelType.Voice, x.Type);
                // TODO: Impl
            });
        }
        [Fact]
        public void ModifyGuildChannelPositionsAsync()
        {
            RunTest(c => c.ModifyGuildChannelPositionsAsync(123, new ModifyGuildChannelPositionParams[]
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }

        [Fact]
        public void GetGuildMembersAsync()
        {
            RunTest(c => c.GetGuildMembersAsync(123, new GetGuildMembersParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }
        [Fact]
        public void GetGuildMemberAsync()
        {
            RunTest(c => c.GetGuildMemberAsync(123, 789), x =>
            {
                Assert.Equal(789UL, x.User.Id.RawValue);
            });
        }
        [Fact]
        public void AddGuildMemberAsync()
        {
            RunTest(c => c.AddGuildMemberAsync(123, 789, new AddGuildMemberParams((Utf8String)"access_token")
            {
                // TODO: Impl
            }), x =>
            {
                Assert.Equal(789UL, x.User.Id.RawValue);
                // TODO: Impl
            });
        }
        [Fact]
        public void RemoveGuildMemberAsync()
        {
            RunTest(c => c.RemoveGuildMemberAsync(123, 789));
        }
        [Fact]
        public void ModifyGuildMemberAsync()
        {
            RunTest(c => c.ModifyGuildMemberAsync(123, 789, new ModifyGuildMemberParams
            {
                // TODO: Impl
            }));
        }
        [Fact]
        public void ModifyCurrentUserNickAsync()
        {
            RunTest(c => c.ModifyCurrentUserNickAsync(123, new ModifyCurrentUserNickParams((Utf8String)"new_nick")));
        }

        [Fact]
        public void AddGuildMemberRoleAsync()
        {
            RunTest(c => c.AddGuildMemberRoleAsync(123, 789, 456));
        }
        [Fact]
        public void RemoveGuildMemberRoleAsync()
        {
            RunTest(c => c.RemoveGuildMemberRoleAsync(123, 789, 456));
        }

        [Fact]
        public void GetGuildBansAsync()
        {
            RunTest(c => c.GetGuildBansAsync(123));
        }
        [Fact]
        public void CreateGuildBanAsync()
        {
            RunTest(c => c.CreateGuildBanAsync(123, 789, new CreateGuildBanParams
            {
                // TODO: Impl
            }));
        }
        [Fact]
        public void DeleteGuildBanAsync()
        {
            RunTest(c => c.DeleteGuildBanAsync(123, 789));
        }

        [Fact]
        public void GetGuildRolesAsync()
        {
            RunTest(c => c.GetGuildRolesAsync(123));
        }
        [Fact]
        public void CreateGuildRoleAsync()
        {
            RunTest(c => c.CreateGuildRoleAsync(123, new CreateGuildRoleParams
            {
                // TODO: Impl
            }), x =>
            {
                // TODO: Impl
            });
        }
        [Fact]
        public void DeleteGuildRoleAsync()
        {
            RunTest(c => c.DeleteGuildRoleAsync(123, 456), x =>
            {
                Assert.Equal(456UL, x.Id.RawValue);
            });
        }
        [Fact]
        public void ModifyGuildRoleAsync()
        {
            RunTest(c => c.ModifyGuildRoleAsync(123, 456, new ModifyGuildRoleParams
            {
                // TODO: Impl
            }), x =>
            {
                Assert.Equal(456UL, x.Id.RawValue);
                // TODO: Impl
            });
        }
        [Fact]
        public void ModifyGuildRolePositionsAsync()
        {
            RunTest(c => c.ModifyGuildRolePositionsAsync(123, new ModifyGuildRolePositionParams[]
            {
                // TODO: Impl
            }));
        }

        [Fact]
        public void GetGuildPruneCountAsync()
        {
            RunTest(c => c.GetGuildPruneCountAsync(123, new GuildPruneParams(30)));
        }
        [Fact]
        public void PruneGuildMembersAsync()
        {
            RunTest(c => c.PruneGuildMembersAsync(123, new GuildPruneParams(30)));
        }

        [Fact]
        public void GetGuildVoiceRegionsAsync()
        {
            RunTest(c => c.GetGuildVoiceRegionsAsync(123));
        }

        [Fact]
        public void GetGuildInvitesAsync()
        {
            RunTest(c => c.GetGuildInvitesAsync(123), x =>
            {
                foreach (var invite in x)
                    Assert.Equal(123UL, invite.Guild.Id.RawValue);
            });
        }

        [Fact]
        public void GetGuildIntegrationsAsync()
        {
            RunTest(c => c.GetGuildIntegrationsAsync(123));
        }
        [Fact]
        public void CreateGuildIntegrationsAsync()
        {
            RunTest(c => c.CreateGuildIntegrationAsync(123, new CreateGuildIntegrationParams(456, (Utf8String)"test_type")));
        }
        [Fact]
        public void DeleteGuildIntegrationAsync()
        {
            RunTest(c => c.DeleteGuildIntegrationAsync(123, 456));
        }
        [Fact]
        public void ModifyGuildIntegrationsAsync()
        {
            RunTest(c => c.ModifyGuildIntegrationAsync(123, 456, new ModifyGuildIntegrationParams
            {
                // TODO: Impl
            }));
        }
        [Fact]
        public void SyncGuildIntegrationsAsync()
        {
            RunTest(c => c.SyncGuildIntegrationAsync(123, 456));
        }

        [Fact]
        public void GetGuildEmbedAsync()
        {
            RunTest(c => c.GetGuildEmbedAsync(123));
        }
        [Fact]
        public void ModifyGuildEmbedAsync()
        {
            RunTest(c => c.ModifyGuildEmbedAsync(123, new ModifyGuildEmbedParams
            {
                ChannelId = new Snowflake(456),
                Enabled = true
            }), x =>
            {
                Assert.Equal(456UL, x.ChannelId?.RawValue ?? 0UL);
                Assert.True(x.Enabled);
            });
        }

        [Fact]
        public void GetGuildVanityUrlAsync()
        {
            RunTest(c => c.GetGuildVanityUrlAsync(123), x =>
            {
                Assert.Equal(123UL, x.Guild.Id.RawValue);
            });
        }
    }
}
