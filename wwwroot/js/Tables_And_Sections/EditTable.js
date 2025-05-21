document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".edit-menu-item-btn").forEach(btn => {
        btn.addEventListener("click", function () {
            const tableId = this.getAttribute("data-id");

            fetch(`/SuperAdmin/UpdateTable/${tableId}`)
                .then(response => response.json())
                .then(data => {
                    console.log("Data received from server:", data);
                    document.getElementById("EditTableId").value = data.id;
                    document.getElementById("editTableSection").value = data.sectionId.toString();
                    document.getElementById("editTableName").value = data.table_number;
                    document.getElementById("editTableCapacity").value = data.capacity;
                    document.getElementById("editTableStatus").value=data.status === 1 ? "Occupied" : "Unoccupied";
                })
                .catch(error => {
                    console.error("Error fetching section data:", error);
                });
        });
    });
});
