console.log("DeleteSection Method called");
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll("[data-bs-target='#deleteSectionModal']").forEach(btn => {
        btn.addEventListener("click", function () {
            const sectionId = this.getAttribute("data-id");
            console.log("data-id to be deleted is: ",sectionId);
            document.getElementById("DeleteSectionId").value = sectionId;
        });
    });
});
