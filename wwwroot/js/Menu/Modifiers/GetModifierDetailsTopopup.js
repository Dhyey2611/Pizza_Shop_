document.addEventListener("DOMContentLoaded", function () {
    const trigger = document.getElementById("addExistingModifiersLink");
    const container = document.getElementById("modifierSelectionContainer");

    // if (trigger && container) {
    //     trigger.addEventListener("click", function (e) {
    //         e.preventDefault();
    //         fetch("/SuperAdmin/GetModifierSelectionPartial")
    //             .then(response => response.text())
    //             .then(html => {
    //                 container.innerHTML = html;
    //                 const modal = new bootstrap.Modal(document.getElementById("selectModifiersModal"));
    //                 modal.show();
    //             })
    //             .catch(error => console.error("Error loading modifiers:", error));
    //     });
    // }
    document.addEventListener("click", function (e) {
        if (e.target.classList.contains("pagination-link")) {
            e.preventDefault();
            const page = e.target.dataset.page;
            loadModifiersPage(page);
        }
    });

    function loadModifiersPage(page) {
        fetch(`/SuperAdmin/GetModifierSelectionPartial?page=${page}`)
            .then(response => response.text())
            .then(html => {
                container.innerHTML = html;
                const modal = new bootstrap.Modal(document.getElementById("selectModifiersModal"));
                modal.show();
            })
            .catch(err => console.error("Error loading modifier data:", err));
    }
});
