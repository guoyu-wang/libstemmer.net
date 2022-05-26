namespace libstemmer.net.unittest
{
   public class Tests
   {
      private string testFileRoot;

      private static bool match( string stemmerName, string language )
      {
         string expectedName = language.Replace( "_", "" ) + "Stemmer";

         return stemmerName.StartsWith( expectedName, StringComparison.CurrentCultureIgnoreCase );
      }

      [SetUp]
      public void Setup()
      {
         var currentDir = Path.GetDirectoryName( TestContext.CurrentContext.TestDirectory );
         Assert.NotNull( currentDir );
         string? repoRoot = Directory.GetParent( currentDir )?.Parent?.Parent?.Parent?.FullName;
         Assert.NotNull( repoRoot );

         this.testFileRoot = Path.Combine( repoRoot, "submodule", "snowball-data" );
      }


      private static void TestSingleLanguage( string language, string testFileRoot )
      {
         var inputFilePath = Path.Combine( testFileRoot, language, @"voc.txt" );
         var outputFilePath = Path.Combine( testFileRoot, language, @"output.txt" );

         // Setup the stemmer
         Stemmer? stemmer =
            typeof( Stemmer ).Assembly.GetTypes()
            .Where( t => t.IsSubclassOf( typeof( Stemmer ) ) && !t.IsAbstract )
            .Where( t => match( t.Name, language ) )
            .Select( t => ( Stemmer? )Activator.CreateInstance( t ) ).FirstOrDefault();

         Assert.IsNotNull( stemmer );

         // Base case, test null and empty input
         var nullStem = stemmer.Stem( null );
         Assert.That( nullStem, Is.EqualTo( string.Empty ) );

         var emptyStem = stemmer.Stem( string.Empty );
         Assert.That( emptyStem, Is.EqualTo( string.Empty ) );

         // Feed some EA strings
         var eaInput = "中文";
         var eaStem = stemmer.Stem( eaInput );
         Assert.That( eaStem, Is.EqualTo( eaInput ), $"{eaInput} and {eaStem} are not the same, input: {eaInput}" );

         using var inputReader = new StreamReader( inputFilePath );
         using var outputReader = new StreamReader( outputFilePath );
         string? input = inputReader.ReadLine();
         string? output = outputReader.ReadLine();
         do
         {
            var stem = stemmer.Stem( input );
            Assert.That( stem, Is.EqualTo( output ), $"{output} and {stem} are not the same, input: {input}" );

            input = inputReader.ReadLine();
            output = outputReader.ReadLine();
            Assert.True( input == null && output == null || input != null && output != null );
         } while( input != null && output != null );
      }

      [Test]
      public void TestEnglish()
      {
         TestSingleLanguage( "english", this.testFileRoot );
      }

      // TODO, unarchive the gz test files
      //[Test]
      //public void TestArabic()
      //{
      //   TestSingleLanguage( "arabic", this.testFileRoot );
      //}

      [Test]
      public void TestArmenian()
      {
         TestSingleLanguage( "armenian", this.testFileRoot );
      }

      [Test]
      public void TestBasque()
      {
         TestSingleLanguage( "basque", this.testFileRoot );
      }

      [Test]
      public void TestCatalan()
      {
         TestSingleLanguage( "catalan", this.testFileRoot );
      }

      [Test]
      public void TestDanish()
      {
         TestSingleLanguage( "danish", this.testFileRoot );
      }

      [Test]
      public void TestDutch()
      {
         TestSingleLanguage( "dutch", this.testFileRoot );
      }

      [Test]
      public void TestFinnish()
      {
         TestSingleLanguage( "finnish", this.testFileRoot );
      }

      [Test]
      public void TestFrench()
      {
         TestSingleLanguage( "french", this.testFileRoot );
      }

      [Test]
      public void TestGerman()
      {
         TestSingleLanguage( "german", this.testFileRoot );
      }

      [Test]
      public void TestGreek()
      {
         TestSingleLanguage( "greek", this.testFileRoot );
      }

      [Test]
      public void TestHindi()
      {
         TestSingleLanguage( "hindi", this.testFileRoot );
      }

      [Test]
      public void TestHungarian()
      {
         TestSingleLanguage( "hungarian", this.testFileRoot );
      }

      [Test]
      public void TestIndonesian()
      {
         TestSingleLanguage( "indonesian", this.testFileRoot );
      }

      [Test]
      public void TestIrish()
      {
         TestSingleLanguage( "irish", this.testFileRoot );
      }

      [Test]
      public void TestItalian()
      {
         TestSingleLanguage( "italian", this.testFileRoot );
      }

      [Test]
      public void TestLithuanian()
      {
         TestSingleLanguage( "lithuanian", this.testFileRoot );
      }

      [Test]
      public void TestNepali()
      {
         TestSingleLanguage( "nepali", this.testFileRoot );
      }

      [Test]
      public void TestNorwegian()
      {
         TestSingleLanguage( "norwegian", this.testFileRoot );
      }

      [Test]
      public void TestPorter()
      {
         TestSingleLanguage( "porter", this.testFileRoot );
      }

      [Test]
      public void TestPortuguese()
      {
         TestSingleLanguage( "portuguese", this.testFileRoot );
      }

      [Test]
      public void TestRomanian()
      {
         TestSingleLanguage( "romanian", this.testFileRoot );
      }

      [Test]
      public void TestRussian()
      {
         TestSingleLanguage( "russian", this.testFileRoot );
      }

      [Test]
      public void TestSerbian()
      {
         TestSingleLanguage( "serbian", this.testFileRoot );
      }

      [Test]
      public void TestSpanish()
      {
         TestSingleLanguage( "spanish", this.testFileRoot );
      }

      [Test]
      public void TestSwedish()
      {
         TestSingleLanguage( "swedish", this.testFileRoot );
      }

      [Test]
      public void TestTamil()
      {
         TestSingleLanguage( "tamil", this.testFileRoot );
      }

      [Test]
      public void TestTurkish()
      {
         TestSingleLanguage( "turkish", this.testFileRoot );
      }

      [Test]
      public void TestYiddish()
      {
         TestSingleLanguage( "yiddish", this.testFileRoot );
      }
   }
}
