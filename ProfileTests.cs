using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoMapperTests
{
	[TestClass]
	public class ProfileTests
	{
		class A
		{
			public string PropFlag { get; set; }
		}

		class B
		{
			public string Prop { get; set; }
		}

		internal class PrefixTestProfile : Profile
		{
			protected override void Configure()
			{
				RecognizePostfixes("Flag");
				CreateMap<A, B>();
			}
		}

		public ProfileTests()
		{
			Mapper.AddProfile<PrefixTestProfile>();
		}

		[TestMethod]
		public void ShouldRecognizeSourcePrefix()
		{
			var testData = new A { PropFlag = "someFlag" };
			var actual = Mapper.Map<B>(testData);
			Assert.AreEqual("someFlag", actual.Prop);
		}
	}
}
