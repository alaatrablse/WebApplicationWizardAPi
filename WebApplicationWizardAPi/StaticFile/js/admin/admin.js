

function getCookies(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for(let i = 0; i <ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) == ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
        return c.substring(name.length, c.length);
      }
    }
    return "";
    titel();
}


function addtolist(user){
    let listTableBody = document.querySelector("#users-list");
    let row = document.createElement("tr");
    var size = listTableBody.getElementsByTagName('tr').length;
    var ss = `<tr name="asdg"><td>
      <div class="form-check form-check-flat mt-0">
        <label class="form-check-label"><input type="checkbox" class="form-check-input" aria-checked="false"><i class="input-helper"></i></label>
      </div></td>
    <td><div class="d-flex"><div><h6 id="header6">${user.name}</h6><p id="userType-${user.id}">${user.type.toLowerCase()}</p></div></div></td>
    <td>
    <div class="btn-group-vertical" role="group" aria-label="Basic example"><div class="btn-group">
    <button type="button" class="btn btn-outline-secondary dropdown-toggle" data-bs-toggle="dropdown" id="but-type-${user.id}">${user.type.toLowerCase()}</button>
    <div class="dropdown-menu"><a class="dropdown-item" onclick='ddd("USER",${user.id})'>User</a>
    <a class="dropdown-item" onclick='ddd("WIZARD CREATOR",${user.id})'>Wizard Creator</a>
    <a class="dropdown-item" onclick='ddd("ADMIN",${user.id})'>Admin</a></div>                          
    </div></div></td><td><h6>${user.email}</h6></td>`;
   
    if(user.type == "WIZARD CREATOR"){
        ss+=`<td><h6>${user.wizards.length}</h6></td></tr>`;
    }
    else{
        ss+=`<td><h6>-</h6></td></tr>`;
    }
   
    row.innerHTML = ss;
    listTableBody.appendChild(row);
}



function getusers(){
    var email = getCookies("usersemail")
    if(email==""){
        localStorage.removeItem("temp");
        window.location.href="home.html";
    }
    var theUrl="/api/users";
    var xhr = new XMLHttpRequest();
    xhr.open('GET', theUrl, true);
    xhr.onload = function(){
        if(this.status == 200){
            var data = JSON.parse(this.responseText);
            var numuser = 0;
            var numwizardcreator = 0;
            var numadmin = 0; 
            for(var i=0;i<data.length;i++){
                if(data[i].type == "USER")
                    numuser++;
                else if(data[i].type == "WIZARD CREATOR")
                    numwizardcreator++;
                else if(data[i].type == "ADMIN")
                    numadmin++;
                if(data[i].email != email){
                    addtolist(data[i]);
                }
                else{
                    document.getElementById("usernum").innerText = data[i].name;
                    document.getElementById("usernum2").innerText = data[i].name;
                    document.getElementById("useremail").innerText = data[i].email;
                }
            }

            localStorage.setItem('temp', JSON.stringify(data));
            document.getElementById("numWizardCreator").innerText=numwizardcreator;
            document.getElementById("numUser").innerText=numuser;
            document.getElementById("numAdmin").innerText=numadmin;
        }
        else{
            console.error();
        }
    }
    xhr.send();
}
getusers()

function ddd(newtype,id){
    var data = JSON.parse(localStorage.getItem('temp'));
    var user = null;
    var index ;
    for(var i=0;i<data.length;i++){
        if(data[i].id == id){
            user = data[i]
            index = i
        }
    }
    if(user != null || user !=""){
        var oldType = user.type;
        user.type = newtype;

        if(oldType != user.type){
            var url = "/api/users/"+id;
            var xhr = new XMLHttpRequest();
            xhr.open("PUT", url);
            xhr.setRequestHeader("Accept", "application/json");
            xhr.setRequestHeader("Content-Type", "application/json");
        
            xhr.onreadystatechange = function () {
                var status = xhr.status;
                if (xhr.readyState === 4) {
                    document.getElementById("but-type-"+user.id).innerHTML = user.type.toLowerCase();
                    document.getElementById("userType-"+user.id).innerHTML = user.type.toLowerCase();
                    var NWC = document.getElementById("numWizardCreator").innerText;
                    var NU = document.getElementById("numUser").innerHTML;
                    var NA = document.getElementById("numAdmin").innerText;
                    if(oldType == "ADMIN"){
                    document.getElementById("numAdmin").innerText=Number(NA)-1;
                    }
                    else if(oldType == "WIZARD CREATOR"){
                        document.getElementById("numWizardCreator").innerText=Number(NWC)-1;
                    }
                    else if(oldType == "USER"){
                        document.getElementById("numUser").innerText=Number(NU)-1;
                    }

                    if(user.type == "ADMIN"){
                        document.getElementById("numAdmin").innerText=Number(NA)+1;
                    }
                    else if(user.type == "WIZARD CREATOR"){
                        document.getElementById("numWizardCreator").innerText=Number(NWC)+1;
                    }
                    else if(user.type == "USER"){
                        document.getElementById("numUser").innerText=Number(NU)+1;
                    } 
                    data[index].type=user.type;
                    localStorage.setItem('temp', JSON.stringify(data));
                }
            };
            xhr.send(JSON.stringify(user));
        }
    }
    
}

function deleteuser(){
    let listTableBody = document.querySelector("#users-list");
    var temp =  JSON.parse(localStorage.getItem('temp'));
    for(var i = 0;i<listTableBody.rows.length;i++){
        var cal=listTableBody.rows[i];
        if(cal.cells[0].getElementsByTagName("input")[0].checked==true){
            var id = temp[i].id
            var email = cal.cells[3].getElementsByTagName("h6")[0].innerHTML;
            if(temp[i].email == email){
                document.getElementById("users-list").deleteRow(i);
                delete temp[i];
                temp.splice(i,i);
                localStorage.setItem('temp', JSON.stringify(temp));
                
                var url = "/api/users/"+id;
                let xhr = new XMLHttpRequest();
                xhr.open("DELETE", url, true);
                xhr.onload = function () {
                    var req = xhr.responseText;
                    if (xhr.readyState == 4 && xhr.status == "200") {
                        listTableBody.deleteRow(i);
                    } else {
                        console.error(req);
                    }
                }
                xhr.send();
            }
        }
    }
}


function deleterequest(id){
    var url = "/api/users/"+id;
    let xhr = new XMLHttpRequest();
    xhr.open("DELETE", url, true);
    xhr.onload = function () {
        var users = JSON.parse(xhr.responseText);
        if (xhr.readyState == 4 && xhr.status == "200") {
            let listTableBody = document.querySelector("#wizards");
            listTableBody.innerHTML="";
            getWizards();
        } else {
            console.error(users);
        }
    }
    xhr.send();

}


function maincheck(){
    let listTableBody = document.querySelector("#users-list");
    for(var i = 0;i<listTableBody.rows.length;i++){
        var cal=listTableBody.rows[i];
        if(cal.cells[0].getElementsByTagName("input")[0].checked==true){
            cal.cells[0].getElementsByTagName("input")[0].checked=false;
        }
        else
            cal.cells[0].getElementsByTagName("input")[0].checked=true;
    }
}


function deleteAllCookies() {
    var cookies = document.cookie.split(";");

    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        var eqPos = cookie.indexOf("=");
        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
}
function exit(){
    deleteAllCookies();
    localStorage.removeItem("temp");
    window.location.href="home.html";
}