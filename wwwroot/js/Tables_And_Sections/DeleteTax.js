// document.addEventListener("DOMContentLoaded", function () {
//     document.querySelectorAll("[data-bs-target='#deleteTaxModal']").forEach(btn => {
//         btn.addEventListener("click", function () {
//             const taxId = this.getAttribute("data-id");
//             document.getElementById("deleteTaxId").value = taxId;
//         });
//     });
// });
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".delete-tax-btn").forEach(btn => {
        btn.addEventListener("click", function () {
            const taxId = this.getAttribute("data-id");
            document.getElementById("deleteTaxId").value = taxId;
        });
    });
});