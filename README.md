<div align="center">
  <h1>ASP.NET Core Web API for a Guest entity - Core 6</h1>
  <h3>A working ASP.NET Core Web API project implementing the CRUD operation for Guest entity.</h3>
</div>

<h3>I implemented this project utilizing two distinct architectures.</h3>

<h3>1) N-Tier Architecture</h3>
<p>  I successfully integrated all endpoints, including security authentication and validations.</p>

<h3>2) CQRS PATTERN</h3>
<ul>
<li>- Using this, I implemented certain endpoints specified in the test with security authentication.</li>
<li>- Due to timing constraints, validations were not added.</li>
<ul>

<p>When you Run project kindly make sure to trust the ASP.NET Core SSL Certificate</p>
<p>
1) Would you like to trust the ASP.NET Core SSL Certificate?
yes
</p>
<p>2) Do you want to install the certificate? 
yes
</p>

<p>Project Swagger Link: https://localhost:7239/swagger/index.html</p>

<p>Attahced Postman collection with all Endpoints</p>

<p>Datebase - SQL Server - (localdb)\\mssqllocaldb;</p>
<p>The API is secured with simple API key header</p>
<p>Header Key - ApiAccessKey</p>
<p>Header Value - teeg_guest_api_key</p>


<p><b>GUEST ENDPOINTS - N TIER ARCHITECTURE</b></p>
<ul>
<li>GET  	- ALL GUEST 	- https://localhost:7239/api/guests</li>
<li>GET  	- GUEST BY ID  	- https://localhost:7239/api/guests/c91651fd-ca1d-41f7-a08a-f334cbfba3e4</li>
<li>POST 	- NEW GUEST  	- https://localhost:7239/api/guests</li>
<li>PUT  	- UPDATE GUEST 	- https://localhost:7239/api/guests</li>
<li>DELETE 	- DELETE GUEST 	- https://localhost:7239/api/guests/c91651fd-ca1d-41f7-a08a-f334cbfba3e4</li>
<li>POST 	- ADD NEW PHONE - https://localhost:7239/api/guests/addphonenumbers</li>
   </ul>
<p><b>GUEST ENDPOINTS - CQRS PATTERN</b></p><ul>
<li>GET  	- ALL GUEST 	- https://localhost:7239/api/guestscqrs</li>
<li>GET  	- GUEST BY ID  	- https://localhost:7239/api/guestscqrs/c91651fd-ca1d-41f7-a08a-f334cbfba3e4</li>
<li>POST 	- NEW GUEST  	- https://localhost:7239/api/guestscqrs</li>
<li>POST 	- ADD NEW PHONE - https://localhost:7239/api/guestscqrs/addphone</li>
  </ul>
