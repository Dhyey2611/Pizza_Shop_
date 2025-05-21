document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll("[data-bs-target='#deleteCategoryModal']").forEach(btn => {
        btn.addEventListener("click", function () {
            const categoryId = this.getAttribute("data-id");
            document.getElementById("deleteCategoryId").value = categoryId;
        });
    });
});