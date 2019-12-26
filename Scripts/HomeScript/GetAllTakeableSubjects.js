//# Match Start from Here #//

class Subject {
    Id = "";
    Name = "";
    IsOptional = "";
    Serial = "";
}
var takeableSubjectList = [];
var addedSubjectList = [];

$(document).ready(function () {
    getAllSubject();
});

function emptyCollection() {
    $("#addedSubjectTable").empty();
    $("#takeSubjectTable").empty();
}

$("#StudentId").change(function () {
    const id = $("#StudentId").val();
    $("#addedSubjectTable").empty();
    $("#takeSubjectTable").empty();
    getSelectedStudentSubject(id);
});

function getSelectedStudentSubject(id) {
    var params = { id: id };
    var url = "../../Home/GetSelectedStudentSubjects";

    $.get(url, params, function (data) {
        if (data.length < 0 || data.count < 0) {
            getAllSubject();
        }
        data.forEach(c => {
            for (let i = 0; i < takeableSubjectList.length; i++) {
                if (takeableSubjectList[i].Id === c.SubjectId) {
                    addedSubjectList.push(takeableSubjectList[i]);
                    takeableSubjectList = jQuery.grep(takeableSubjectList,
                        function (value) {
                            return value !== takeableSubjectList[i];
                        });
                }
            }
        });

        makeTableEffect();
        addedTableEffect();
    }).fail(function (err) {
        alert(err);
    });
}

function getAllSubject() {
    $.ajax({
        url: "../../Home/GetTakeableSubjects",
        type: "GET",
        data: JSON.stringify(),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data !== false && data !== undefined && data.length > 0) {
                sl = 1;
                $.each(data, function (k, v) {
                    let sub = new Subject();
                    sub.Id = v.Id;
                    sub.Name = v.Name;
                    sub.IsOptional = v.IsOptional;
                    sub.Serial = sl;
                    takeableSubjectList.push(sub);
                    sl++;
                });
            }
        }, error: function (err) {
            alert(err);
        }
    });
}

function makeTableEffect() {
    $("#takeSubjectTable").empty();

    if (takeableSubjectList.length > 0) {
        for (let i = 0; i < takeableSubjectList.length; i++) {
            takeableTableRowEffect(takeableSubjectList[i].Serial, takeableSubjectList[i].Name, takeableSubjectList[i].IsOptional, takeableSubjectList[i].Id);
        }
    }
}

function takeableTableRowEffect(serial, subjectName, subjectIsOptional, id) {
    var serialCell = "<td>" + serial + "</td>";
    var subjectNameCell = "<td>" + subjectName + "</td>";
    var subjectOptionalCheckBoxCell = "<td> <input type='checkbox' id='isTrueOrFalse_" + serial + "' disabled> </td>";
    var options = "<td align='center'><button type='button' class='btn btn-success' id='addedSubjectArrow' onClick='addedSubjectArrowCell(" + id + ")'> <span class='glyphicon glyphicon-chevron-right'></span></button>    </td>";
    var createNewRow = "<tr> " + serialCell + subjectNameCell + subjectOptionalCheckBoxCell + options + " </tr>";

    $("#takeSubjectTable").append(createNewRow);

    checkBoxTrueOrFalse(subjectIsOptional, serial);
}

function addedSubjectArrowCell(sl) {
    GetAddedSub(sl);
    $("#addedSubjectTable").empty();
    makeTableEffect();
    addedTableEffect();
}

function addedTableEffect() {
    if (addedSubjectList.length > 0) {
        if (addedSubjectList.length > 0) {
            for (let i = 0; i < addedSubjectList.length; i++) {
                addedTableRowEffect(addedSubjectList[i].Serial, addedSubjectList[i].Name, addedSubjectList[i].IsOptional, addedSubjectList[i].Id);
            }
        }
    }
}

function GetAddedSub(id) {
    for (let i = 0; i < takeableSubjectList.length; i++) {
        if (takeableSubjectList[i].Id === id) {
            addedSubjectList.push(takeableSubjectList[i]);
            takeableSubjectList = jQuery.grep(takeableSubjectList,
                function (value) {
                    return value != takeableSubjectList[i];
                });
        }
    }
}

function addedTableRowEffect(serial, subjectName, subjectIsOptional, id) {
    var serialCell = "<td><input type='hidden' id='Subjects_" + serial + "' name='Subjects.Index' value='" + id + "'/>" + serial + "</td>";
    var subjectNameCell = "<td><input type='hidden' id='Subjects_" + serial + "'  name='Subjects[" + serial + "].Name' value='" + subjectName + "'/>" + subjectName + "</td>";
    var subjectOptionalCheckBoxCell = "<td>" +
        "<input type='checkbox' name='Subjects[" + serial + "].IsOptional' value='true' id='addedTrueOrFalse_" + serial + "' > " +
        "<input type='hidden' name='Subjects[" + serial + "].IsOptional' value='false'>" +
        "</td>";
    var options = "<td align='center'><button type='button' class='btn btn-success' id='deleteFromAdded' onClick='deleteFromAddedCell(" + id + ")'> <span class='glyphicon glyphicon-chevron-left'></span></button>    </td>";
    var createNewRow = "<tr> " + serialCell + subjectNameCell + subjectOptionalCheckBoxCell + options + " </tr>";

    $("#addedSubjectTable").append(createNewRow);

    checkAddedTrueOrFalse(subjectIsOptional, serial);

    if (subjectIsOptional) {
        $("#addedTrueOrFalse_" + serial).prop("checked", true);
    } else {
        $("#addedTrueOrFalse_" + serial).prop("checked", false);
    }
}

function checkAddedTrueOrFalse(subject, sl) {
    if (subject === true) {
        $("#addedTrueOrFalse_" + sl).attr("checked", true);
    } else {
        $("#addedTrueOrFalse_" + sl).attr("checked", false);
    }
}

function checkBoxTrueOrFalse(subject, sl) {
    if (subject === true) {
        $("#isTrueOrFalse_" + sl).attr("checked", true);
    } else {
        $("#isTrueOrFalse_" + sl).attr("checked", false);
    }
}

function deleteFromAddedCell(sl) {
    GetRemovedSub(sl);

    $("#takeSubjectTable").empty();
    $("#addedSubjectTable").empty();

    makeTableEffect();
    addedTableEffect();

    if (takeableSubjectList.length > 0) {
        for (let i = 0; i < takeableSubjectList.length; i++) {
            takeableSubjectList(takeableSubjectList[i].Serial, takeableSubjectList[i].Name, takeableSubjectList[i].IsOptional, addedSubjectList[i].Id);
        }
    }
}

function GetRemovedSub(id) {
    for (let i = 0; i < addedSubjectList.length; i++) {
        if (addedSubjectList[i].Id === id) {
            takeableSubjectList.push(addedSubjectList[i]);

            addedSubjectList = jQuery.grep(addedSubjectList,
                function (value) {
                    return value != addedSubjectList[i];
                });
        }
    }
}