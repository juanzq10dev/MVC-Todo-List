// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function validateTask() {
    var taskName = document.getElementById("nameInput").value.trim();
    if (taskName == "") {
        alert('Please enter a valid name');
        return false;
    }

    return true;
}

function deleteTask(taskId) {
    fetch(`/Home/DeleteTask/${taskId}`, {
        method: "DELETE",
        headers: {
            'Content-Type': 'application/json'
        },
    }).then(response => {
        if (response.ok) {
            location.reload();
        } else {
            alert("Failed to delete task");
        }
    })
}

function updateTaskName(taskId) {
    let newName = document.getElementById(`blockquote ${taskId}`).innerText.trim();
    fetch(`/Home/UpdateTask/${taskId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Name: newName })
    })
    .then(response => {
        if (response.ok) {
            alert("Name updated succesfully")
        } else {
            alert("Could not update the task")
        }
    });
}

function updateCompletedValue(taskId, checked) {
    fetch(`/Home/UpdateTaskStatus/${taskId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Completed: checked })
    })
        .then(response => {
            if (response.ok) {
            } else {
                alert("Could not update the task")
            }
        });
}


function showEditButton(blockquote) {
    var taskId = blockquote.getAttribute('data-task-id');
    var editButton = document.querySelector(`[data-task-id="${taskId}"] + .edit-button`);

    if (blockquote.innerText !== blockquote.dataset.originalValue) {
        editButton.classList.remove('d-none');
    } else {
        editButton.classList.add('d-none');
    }
}

function enableSaveButton(taskId, blockquote) {
    const saveButton = document.getElementById(taskId);
    if (blockquote.innerText !== blockquote.dataset.originalValue) {
        saveButton.disabled = false;
    } else {
        saveButton.disabled = true;
    }    
}