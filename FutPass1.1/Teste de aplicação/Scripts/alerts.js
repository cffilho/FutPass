$('#btnDeleteClub').click(function () {
    alert("Club successfully removed!");
});

$('#btnDeleteManager').click(function () {
    alert("Manager successfully removed!");
});

$('#btnDeletePlayer').click(function () {
    alert("Player successfully removed!");
});

$('#btnDeleteUser').click(function () {
    alert("User successfully removed!");
});

function create() {
    var resposta = confirm("Are you sure?");
    if (resposta == true) {
        alert("Successfully create");
        return true;
    } else {
        return false; 
    }
}

function edit() {
    var resposta = confirm("Are you sure?");
    if (resposta == true) {
        alert("Successfully edited");
        return true;
    } else {
        return false;
    }
}