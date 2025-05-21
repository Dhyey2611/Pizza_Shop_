// document.getElementById("modifierSearch").addEventListener("keyup", function () {
//     const searchTerm = this.value.toLowerCase();
//     const rows = document.querySelectorAll(".modifier-row");

//     rows.forEach(row => {
//         const name = row.getAttribute("data-name");
//         row.style.display = name.includes(searchTerm) ? "" : "none";
//     });
// });
// document.addEventListener("DOMContentLoaded", function () {
//     const selectModal = document.getElementById("selectModifiersModal");
//     const addBtn = document.getElementById("addModifiersBtn");
//     let reopenParent = false;

//     // Set flag when Save (Add) is clicked
//     addBtn.addEventListener("click", function () {
//         reopenParent = true;
//     });

//     // On modal close, decide whether to reopen first modal
//     selectModal.addEventListener("hidden.bs.modal", function () {
//         const firstModal = document.getElementById("AddMenuItemModal");

//         if (reopenParent) {
//             new bootstrap.Modal(firstModal).show();
//         } else {
//             const modalInstance = bootstrap.Modal.getInstance(firstModal);
//             if (modalInstance) {
//                 modalInstance.hide(); // Ensure parent is hidden too
//             }
//         }

//         // Reset flag and optional UI cleanup
//         reopenParent = false;
//         document.getElementById("modifierSearch").value = "";
//         document.querySelectorAll(".modifier-row").forEach(row => row.style.display = "");
//         document.querySelectorAll(".modifier-checkbox").forEach(cb => cb.checked = false);
//     });
// });
document.addEventListener("DOMContentLoaded", function () {
    // 🔍 Search Bar Filtering
    document.getElementById("modifierSearch").addEventListener("keyup", function () {
        const searchTerm = this.value.toLowerCase();
        const rows = document.querySelectorAll(".modifier-row");

        rows.forEach(row => {
            const name = row.getAttribute("data-name");
            row.style.display = name.includes(searchTerm) ? "" : "none";
        });
    });

    // 🔄 Modal Handling: Save vs Cancel/X
    const selectModal = document.getElementById("selectModifiersModal");
    const addBtn = document.getElementById("addModifiersBtn");
    let reopenParent = false;

    // Set flag when Save (Add) is clicked
    addBtn.addEventListener("click", function () {
        reopenParent = true;
    });

    // On modal close, decide whether to reopen first modal
    selectModal.addEventListener("hidden.bs.modal", function () {
        const firstModal = document.getElementById("addModifierGroupModal");

        if (reopenParent) {
            new bootstrap.Modal(firstModal).show();
        } else {
            const modalInstance = bootstrap.Modal.getInstance(firstModal);
            if (modalInstance) {
                modalInstance.hide(); // Ensure parent is hidden too
            }
        }

        // Reset flag and optional UI cleanup
        reopenParent = false;
        document.getElementById("modifierSearch").value = "";
        document.querySelectorAll(".modifier-row").forEach(row => row.style.display = "");
        document.querySelectorAll(".modifier-checkbox").forEach(cb => cb.checked = false);
    });
});

