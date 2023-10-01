var employeeList = [];
var is_asc = true;

function getEmployees() {
  callData(onSuccess, onError);
}

function callData(onSuccess, onError) {
  $.ajax({
    url: "https://jsonplaceholder.typicode.com/users",
    type: "GET",
    dataType: "json",
    success: onSuccess,
    error: onError,
  });
}

function onSuccess(data) {
  employeeList = data == null ? [] : data instanceof Array ? data : [data];
  if (employeeList.length > 0) {
    addDataToTable();
  }
}

function onError() {
  alert("Error occured while getting employees");
}

function addDataToTable() {
  $("#employeeList").empty();
  $("#employeeList").append("<tr><th>ID</th><th>Name</th><th>Username</th><th>Email</th><th>Address</th><th>Phone</th><th>Website</th><th>Company</th></tr>");

  if (employeeList.length > 0) {
    $.each(employeeList, function (index, employee) {
      var address =
        employee.address.suite +
        ", " +
        employee.address.street +
        ", " +
        employee.address.city
        $("#employeeList").append(
        "<tr><td>" +
          employee.id +
          "</td><td>" +
          employee.name +
          "</td><td>" +
          employee.username +
          "</td><td>" +
          employee.email +
          "</td><td>" +
          address +
          "</td><td>" +
          employee.phone +
          "</td><td>" +
          employee.website +
          "</td><td>" +
          employee.company.name +
          "</td></tr>"
      );
    });
  } else {
    $("#employeeList").append("<tr><td colspan='8' id='noData'>No employees found</td></tr>");
  }
}

function sortTable() {
  if (employeeList.length > 0) {
    if (is_asc) {
      employeeList.sort(function (a, b) {
        return a.name.localeCompare(b.name);
      });
      is_asc = false;
    } else {
      employeeList.sort(function (a, b) {
        return b.name.localeCompare(a.name);
      });
      is_asc = true;
    }
    addDataToTable();
  } else {
    alert("No employees to sort");
  }
}

function clearTable() {
  employeeList = [];
  addDataToTable();
}

function searchTable() {
  var searchValue = $("#search").val();
  if (searchValue != null && searchValue != "") {
    var filteredList = employeeList.filter(function (employee) {
      return employee.name.toLowerCase().includes(searchValue.toLowerCase());
    });
    employeeList = filteredList;
    addDataToTable();
  }
}
