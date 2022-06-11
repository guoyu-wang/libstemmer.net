// Copyright (c) Guoyu Wang. All rights reserved.
// Licensed under the MIT License.

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


      private static void TestSingleLanguageInternal( string language, string testFileRoot )
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

         // Feed some Arabic strings
         var arabicInput = "عربى";
         var arabicStem = stemmer.Stem( arabicInput );
         Assert.That( eaStem, Is.EqualTo( arabicInput ), $"{arabicInput} and {arabicStem} are not the same, input: {arabicInput}" );

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

      [TestCase( "english" )]
      // [TestCase( "arabic" )] // TODO, unarchive the gz test files for arabic
      [TestCase( "armenian" )]
      [TestCase( "basque" )]
      [TestCase( "catalan" )]
      [TestCase( "danish" )]
      [TestCase( "dutch" )]
      [TestCase( "finnish" )]
      [TestCase( "french" )]
      [TestCase( "german" )]
      [TestCase( "greek" )]
      [TestCase( "hindi" )]
      [TestCase( "hungarian" )]
      [TestCase( "indonesian" )]
      [TestCase( "irish" )]
      [TestCase( "italian" )]
      [TestCase( "lithuanian" )]
      [TestCase( "nepali" )]
      [TestCase( "norwegian" )]
      [TestCase( "porter" )]
      [TestCase( "portuguese" )]
      [TestCase( "romanian" )]
      [TestCase( "russian" )]
      [TestCase( "serbian" )]
      [TestCase( "spanish" )]
      [TestCase( "swedish" )]
      [TestCase( "tamil" )]
      [TestCase( "turkish" )]
      [TestCase( "yiddish" )]
      public void TestSingleLanguage( string language )
      {
         TestSingleLanguageInternal( language, this.testFileRoot );
      }
   }
}
