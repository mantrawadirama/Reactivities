<h2>Entire application is developed using vscode</h2>
<h4>Technologies</h4>
<ul>
<li>.Net Core</li>
<li>Clean architecture</li>
<li>My Sql</li>
<li>Thin API controller structure</li>
<li>CQRS pattern</li>
<li>MediatR pattern</li>
</ul>
### During Dev used Sqlite
This project was bootstrapped with [Install Sqlite on Ubuntu](https://linuxhint.com/install_sqlite_browser_ubuntu_1804/).

<table>
<tr>
<th colspan="2">commands used
</th>
</tr>
<tr>
<td>Create solution</td>
<td>dotnet new sln</td>
</tr>
<tr>
<td>Create class Lib</td>
<td>Ex: dotnet new classlib -n Reactivities.Domain</td>
</tr>
<tr>
<td>Create Web API</td>
<td>dotnet new webapi -n Reactivities.API</td>
</tr>
<tr>
<td>Add Project to Solution </td>
<td>dotnet sln add Reactivities.Domain/<br/>
dotnet sln add Reactivities.Application/<br/>
dotnet sln add Reactivities.Persistence/</td>
</tr>
<tr>
<td>Add Domain reference to Persistance proejct  </td>
<td>dotnet add reference ../Reactivities.Domain/</td>
</tr>
<tr>
<td>List Proejcts in solution  </td>
<td>dotnet sln list</td>
</tr>
<tr>
<td>DB changes  </td>
<td>dotnet ef migrations add InitialCreate -p Reactivities.Persistence/ -s Reactivities.API/</td>
</tr>
<tr>
<td>Starting API </td>
<td>dotnet run -p Reactivities.API</td>
</tr>
<tr>
<td>To check for changes and run API </td>
<td>dotnet watch run</td>
</tr>
</table>
