using System;
using Devcat.Core;
using Devcat.Core.Net.Message;
using MMOServer;
using NUnit.Framework;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.EndPointNetwork;

namespace HeroesChannelServer
{
	[TestFixture]
	public class MapPlayerTest
	{
		[Test]
		public void Test()
		{
			Map map = new Map("t01", "t01_partition.dat");
			if (!map.Vaild)
			{
				Assert.IsTrue(false);
			}
			Channel channel = new Channel();
			map.Build(channel);
			channel.ConfirmRegions(3);
			Player player = new Player(1L, channel, null);
			player.SendMessage += delegate(object sender, EventArgs<Packet> arg)
			{
				Console.WriteLine("Player1 : {0}", arg.Value);
			};
			player.Enter(6479215318592913409L, new CharacterSummary(1L, 1, "Player1", 1, BaseCharacter.Kalok, 5, 100, 2000, 0, 0, null, 0, 0, 0, "", 0, "", 0, false, false, false, false, 0, false, 0, VocationEnum.None, 0, 0, 0, 0, 0, 0, null, false, "", 0, 0, 0, 0), default(ActionSync));
			Player player2 = new Player(2L, channel, null);
			player2.SendMessage += delegate(object sender, EventArgs<Packet> arg)
			{
				Console.WriteLine("Player2 : {0}", arg.Value);
			};
			player2.Enter(6479215318592913409L, new CharacterSummary(1L, 1, "Player2", 1, BaseCharacter.Lethita, 5, 100, 2000, 0, 0, null, 0, 0, 0, "", 0, "", 0, false, false, false, false, 0, false, 0, VocationEnum.None, 0, 0, 0, 0, 0, 0, null, false, "", 0, 0, 0, 0), default(ActionSync));
			Console.WriteLine("----");
			player.Move(6479215318592913410L);
			player2.Action(new ActionSync
			{
				ActionStateIndex = 2
			});
			player.UpdateLook(new CharacterSummary(1L, 1, "Player1", 1, BaseCharacter.Kalok, 6, 100, 2000, 0, 0, null, 0, 0, 0, "", 0, "", 0, false, false, false, false, 0, false, 0, VocationEnum.None, 0, 0, 0, 0, 0, 0, null, false, "", 0, 0, 0, 0));
			Console.WriteLine("----");
			player2.Move(6479215318592913421L);
			Console.WriteLine("----");
			player.Move(6479215318592913411L);
		}
	}
}
