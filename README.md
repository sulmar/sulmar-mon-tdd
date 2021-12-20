
| NUnit 3.x            | MSTest v2.x.         | xUnit.net 2.x             | Comments                                           |
| -------------------- | -------------------- | ------------------------- | -------------------------------------------------- |
| \[Test\]             | \[TestMethod\]       | \[Fact\]                  | Marks a test method.                               |
| \[TestFixture\]      | \[TestClass\]        | n/a                       | Marks a test class.                                |
| `[SetUp]`            | \[TestInitialize\]   | Constructor               | Triggered before every test case.                  |
| \[TearDown\]         | \[TestCleanup\]      | IDisposable.Dispose       | Triggered after every test case.                   |
| \[OneTimeSetUp\]     | \[ClassInitialize\]  | IClassFixture<T>          | One-time triggered method before test cases start. |
| \[OneTimeTearDown\]  | \[ClassCleanup\]     | IClassFixture<T>          | One-time triggered method after test cases end.    |
| \[Ignore("reason")\] | \[Ignore\]           | \[Fact(Skip="reason")\]   | Ignores a test case.                               |
| \[Property\]         | \[TestProperty\]     | \[Trait\]                 | Sets arbitrary metadata on a test.                 |
| \[Theory\]           | \[DataRow\]          | \[Theory\]                | Configures a data-driven test.                     |
| \[Category("")\]     | \[TestCategory("")\] | \[Trait("Category", "")\] | Categorizes the test cases or classes.             |
