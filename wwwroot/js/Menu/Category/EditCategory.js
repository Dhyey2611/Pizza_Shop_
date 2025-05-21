document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".edit-category-btn").forEach(btn => {
        btn.addEventListener("click", function () {
            const categoryId = this.getAttribute("data-id");

            fetch(`/SuperAdmin/EditCategory/${categoryId}`)
                .then(response => response.json())
                .then(data => {
                    document.getElementById("editCategoryId").value = data.id;
                    document.getElementById("editCategoryName").value = data.name;
                    document.getElementById("editCategoryDescription").value = data.description;
                })
                .catch(error => {
                    console.error("Error fetching category data:", error);
                });
        });
    });
});