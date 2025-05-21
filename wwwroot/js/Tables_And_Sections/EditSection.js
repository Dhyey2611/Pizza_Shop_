document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".edit-category-btn").forEach(btn => {
        btn.addEventListener("click", function () {
            const sectionId = this.getAttribute("data-id");

            fetch(`/SuperAdmin/EditSection/${sectionId}`)
                .then(response => response.json())
                .then(data => {
                    console.log("Data received from server:", data);
                    document.getElementById("EditSectionId").value = data.id;
                    document.getElementById("EditSectionName").value = data.name;
                    document.getElementById("EditSectionDescription").value = data.description;
                })
                .catch(error => {
                    console.error("Error fetching section data:", error);
                });
        });
    });
});
