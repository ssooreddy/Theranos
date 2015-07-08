						AlbumSongAPI
--------------------------------------------------------------------------------------------------------------------------Architecure
1. I have developed this API using repository pattern.
2. In repository pattern we have 
	a) Repositories - To perform database operations.
	b) Providers - To perform business logic or object orchestration as per client needs.
3. Repositories and providers are implemented from the respective interfaces, which makes this approach modular.
4. We also have common projects, which will be used by repositories, providers and API.
	a) Domain - Database objects to classes
	b) DTO - Data transfer objects as per the client
	c) Interfaces - Which defines what to be done in each repository, provider but no implementation.
5. Repositories, providers, database connection factories, loggers are injected into the container as interface instances.
6. This injection type implementation allows development and testing of individual repositories. This is done in APIHost which is present in App_Host.
7. We have overridden configure method to add over own injections to Funq container.

Development
1. To handle large xml files, used stream reader to avoid memory over flow.
2. Locked the file only at last saving step to avoid being locked most of the time and avoid dirty writes.
3. Reading is enabled always and so is concurrent access.
4. Getting album and song details without the source being mentioned by client.

DLLS
 In case, if NuGet doesn't download dlls needed, they are present in ExternalDlls folder to add it to the projects.


