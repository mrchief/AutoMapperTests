using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoMapperTests
{
	internal class A
	{
		public string PropFlag { get; set; }
	}

	internal class B
	{
		public string Prop { get; set; }
	}

	internal class PrefixTestProfile : Profile
	{
		protected override void Configure()
		{
			RecognizePostfixes("Flag");
			Mapper.CreateMap<A, B>();
		}
	}

	[TestClass]
	public class ProfileTests
	{

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
