What do you need to do to work locally:

Package folder is not there!!! Enable NuGet Package Restore and build the solution.

SPA Project -> on App/Services, change the 'urlBase' on all services to your WebAPI Project base url.
	       
WebAPI Project -> on web.config, change the connection string (Don't change I'ts name, Entity Framework will look for "IntegritasContext" connection) and set as StartUp Project.

Infra.Data Project -> run a Entity Framework Migration to create data base.

Made with:

DDD (Domain Driven Design);

Single Page Application (AngularJS + Bootstrap); 
View models validation with Data Annotations; 
View models mapping with AutoMapper; 

Enitity Framework: Code First Migrations; Fluent API Mapping;

Dependency Injection: Ninject + Microsoft ServiceLocation;

Generic Repository Pattern; 
Unity of Work Pattern; 
One Context Per Request Pattern; 
Specification Pattern;
