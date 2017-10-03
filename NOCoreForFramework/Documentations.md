Network Observability tool
==========================
---


# Introduction
__Networks__ can be found everywhere, and they grow fast. As they grow, network administrators find it harder
 to monitor these networks. Monitoring networks can help optimising them and prevent attacks and network failures.   
There are quite a lot of software programs that can be connected to a network device and help network 
administrators observe the traffic passing through – mainly called **observers**. However, none of 
them can provide information such as where the best place to connect an observer is.
### Who this app is for
This application has been tailored to help network administrators who are interested in 
observing the largest segment of a network with the minimum number of observers.   

### Problem description
Currently there are no methods or formulas that can calculate the minimum number 
of observers to be used to monitor a segment of a network. Additionally, once a set of observers 
are connected to the network, it is not easy to determine their maximum coverage.   
It is also important to ensure that every single network device is observed and 
none of them are missed, and if there are devices that their communications cannot be observed, 
they should be exposed.


### The suggested solution
__Professor Colin Fidge__ and __Dr Ernest Foo__ - the project owners suggested that 
a _**'Network visualisation tool'**_ - that 
allows the users to see what observers can see, is a potentially workable solution.    
The tool would allow the network administrators draw _graphs_ representing the actual 
networks, add and remove certain ___attributes___ and ___rules___ and run the __algorithm__ based on certain 
assumptions, and select certain nodes as __observers__. The tool then should display all the 
communications between all the nodes that can be 'observed'.   


### Other Applications
The tool that we have created has properties that allows users use it not only for communication 
networks but also for a wide range of other types of networks such as road networks, water and 
electricity supply networks etc.  

---

# Network Observability Tools
The Network Observability Tools uses the _**general properties**_ of a network and allows users to use 
the application for their desired type of the network by adding _**attributes**_ to the **vertices** and 
the **edges**. Hence, the basic knowledge of networks is necessary to use this application.    
In this document the general term _**network**_ mainly refers to _**communications networks**_.
### What it is
The application is a `C#` project and currently is `Windows-based` only.    
The application consists of several components. `Windows Presentation Foundation WPF` is used to 
render user interfaces. There is a separation between the **front-end logic** and the **back-end logic** 
and they interact with each other through an `application programming interface`.  
The back-end logic is compiled in a separate project as is loaded into the front-end 
project as a **Dynamic-link Library** `*.dll` file.

### How to use it
__Drawing a graph:__ Using the application is very easy and does not require much training. The user can drag one or 
more ___nodes___ from the toolbar section onto the main ___canvas___, and connect them to each other by 
***right click***ing on the _source node_ first and select `Connect from here` and then ___right click___ on 
the _destination_ node and select `Connect to here`.  
   
![alt text](https://github.com/fanrice123/NOCoreForFramework/blob/master/NOCoreForFramework/img/node.PNG?raw=true "Select node")
  
![alt text]( https://github.com/fanrice123/NOCoreForFramework/blob/master/NOCoreForFramework/img//connect_from.PNG?raw=true "Connect from")
 
![alt text]( \img\connect_to.png "Connect to")
   
![alt text]( \img\connected_nodes.png "Connected node")

__Setting attributes:__ Key value pairs can be added to the edges as ___attributes___. Users can also select either a _node_ or an _edge_ by clicking on them and view and set 
some attributes. Attribute types can be __descriptive__ or __numerical__. Users 
can also select a node to be an __observer__ from here by checking the `Observer` checkbox. 
  
![alt text]( \img\selected_edge.png "Selected edge")   
  
![alt text]( \img\add_attr.png "Add attribute")   
  
![alt text]( \img\side_panel.png "Side Panel")   
   
![alt text]( \img\is_observer.png "Is observer")   
   
For instance, devices’ names can be descriptive attributes, whereas, supported 
protocols and variable properties of the entities such as cost and speed can be of type numerical.   
   
__Saving and loading graphs from/to file:__ Once the user is happy with the graph on the canvas, they can save it to an `XML` file by clicking 
on `File > Save`. Additionally, they can load compatible `XML` files into the program.   
   
![alt text]( \img\save.png "Save")   
  
__Running the algorithm:__ After drawing the graph on the canvas, user can hit the `Start` and then `Run` button 
to run the algorithm. At this point user can select some _numerical attributes_ and specify a 
range for each so the algorithm can use the attributes to find the best _paths_.   
   
![alt text]( \img\start.png "Start")   
   
__Output:__ The algorithm then searches for all the possible paths between each ___pair___ of nodes 
and connects a __line__ between those pairs if there are paths that contains an ___observer___ between the pairs. 
The application is also able to calculate an _approximate percentage of the traffic_ that could possible 
go through each path. This output will be shown in a separate pop up `Window`.
# How it works
One of the clients' requirements was to be able to load different libraries onto the application so that 
they can use different algorithms. Therefore, the application consists of two _front-end_ and _back-end_ 
parts. This allows us to easily import the back-end as a _library_ into the application and make 
changes if needed or load completely new ones.  
   
   

*data structures used 

