using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoMapperTests
{


	[TestClass]
	public class CreateMapTests
	{
		private class A
		{
			public string PropID { get; set; }
			public string PropB { get; set; }
		}

		private class B
		{
			public string PropId { get; set; }
			public string PropB { get; set; }
			public string PropC { get; set; }
		}

		internal class CreateMapTestProfile : Profile
		{
			protected override void Configure()
			{
				// will complain about Unmapped member PropC when AssertConfigurationIsValid is called.
				CreateMap<A, B>();
			}
		}

		internal class CreateMapTestWithSourceMemberListProfile : Profile
		{
			protected override void Configure()
			{
				// will complain about Unmapped member PropID when AssertConfigurationIsValid is called.
				CreateMap<A, B>(MemberList.Source);

			}
		}

		[TestMethod]
		public void ShouldMapSourceList()
		{
			Mapper.AddProfile<CreateMapTestWithSourceMemberListProfile>();
			//Mapper.AssertConfigurationIsValid();

			var a = new A
			{
				PropID = "someId",
				PropB = "random",
			};

			var actual = Mapper.Map<B>(a);

			Assert.AreEqual("someId", actual.PropId);

		}

		[TestMethod]
		public void ShouldValidateAgainstSourceListOnly()
		{
			Mapper.AddProfile<CreateMapTestWithSourceMemberListProfile>();
			Mapper.AssertConfigurationIsValid();

			// if we got here without exceptions, it means we're good!
			Assert.IsTrue(true);
		}
	}
}
