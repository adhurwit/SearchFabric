# SearchFabric

A sample ServiceFabric application that uses the ReliableRAMStore project for Lucene to create a scalable, fault-tolerant search engine composed of two services. 

This is currently a Proof of Concept. 

**Please let me know if you are interested in moving this forward**

The ReliableRAMStore repository is located here
[ReliableRAMStore](http://github.com)


**Versions**

- v0.1 - 8/15 - Proof of Concept**



## SearchFabric.API
A Stateless ServiceFabric service that exposes a couple of API methods via HTTP. A POST method to add a document to the index, and a GET method to run a query on the index. 


## SearchFabric.Index
A Stateful ServiceFabric service that hosts the ReliableRAMStore and exposes two methods: one to add a document to the index, another to query the index.  


## TODO
- Flesh out the API
- Provide way to define the index structure
- Support multiple indices

