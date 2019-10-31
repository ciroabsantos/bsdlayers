# bsdlayers
Common Objects For Projects Using Business, Service And Data Layers  

The objective of this project is to create reusable objects, contributing to standardize the development of different applications in an organization, 

making easier the maintenance, and faster the development of new features. 

#### Business Layer:
###### In this layer is implemented objects that contains domain business rules 

- Model:  Here are defined the domain model:  
	- Entities,  Value-Objects, ...

	- Commands: Here are created objects to encapsulate business operations.  

		- Create Entity Xpto 
		- Resolve Report Data, ….

- Specs: Here are created objects that validate the state of model objects for specific situations: 

	- Spec to verify if Xpto is valid to be persisted, 
	- Spec to verify if Report eligible to be published, … 

#### Service Layer: 
###### Here are created objects that implements application workflows 

- Services: Implementation of applications workflows 

	- 1 Workflow == 1 Service == 1 API Endpoint 

	- A Service orchestrates the execution of one or more commands 

	- A Service is responsible to manage transactions 

		- Service Create Xpto: 

		- Start transaction 

		- Create and execute command to Insert Xpto 

		- Create and execute command to notify manager 

		- Commit transaction

- Service Model:  Definition of Services requests and responses DTOs 

- Data Layer: Here are created objects responsible for transport data to database, file system, APIs, ...

	- DAO: Objects where data operations are implemented 

	- Data Model: Where DTOS for data operations are defined.
