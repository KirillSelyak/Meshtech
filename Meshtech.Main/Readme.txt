1) Log files path are set by command line arguments.
Debug mode contains few predefined, hardcoded file paths.

2) There is used dependency injection approach.
Dependency injection containers are not used. Instead there is lightweight BootStrapper class.

3) There is used earlier abstraction and Open Closed Principle. 
When need to start using live log streams then new implementation of IStreamReaderFactory is needed.
Just implement it and inject.

4) There are some tests. Ideally some tests for MeshtechTreeConstructor are needed as well.
