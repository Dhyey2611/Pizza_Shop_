// document.addEventListener("DOMContentLoaded", function () {
//     const buttons = document.querySelectorAll(".modifier-btn");
//     const listContainer = document.getElementById("modifierList");
//     const wrapper = document.getElementById("modifierDetails");
//     let selectedModifiers = [];

//     buttons.forEach(btn => {
//         btn.addEventListener("click", function () {
//             const groupId = this.dataset.groupId;

//             fetch(`/SuperAdmin/GetModifiersByGroup?groupId=${groupId}`)
//                 .then(response => response.json())
//                 .then(data => {
//                     listContainer.innerHTML = "";
//                     if (data.length > 0) {
//                         wrapper.style.display = "block";
//                         data.forEach(mod => {
//                             const li = document.createElement("li");
//                             li.className = "list-group-item";

//                             li.innerHTML = `
//                                 <div class="d-flex align-items-center gap-2">
//                                     <input type="checkbox" class="modifier-checkbox" data-id="${mod.modifierId}">
//                                     <span>${mod.name} - ₹${mod.price}</span>
//                                     <select class="form-select form-select-sm mean-select" style="width: 70px;">
//                                         ${[...Array(101).keys()].map(i => `<option value="${i}">${i}</option>`).join('')}
//                                     </select>
//                                     <select class="form-select form-select-sm max-select" style="width: 70px;">
//                                         ${[...Array(101).keys()].map(i => `<option value="${i}">${i}</option>`).join('')}
//                                     </select>
//                                 </div>
//                             `;

//                             listContainer.appendChild(li);
//                         });
//                     } else {
//                         wrapper.style.display = "none";
//                     }
//                 });
//         });
//     });

//     document.querySelector('#AddMenuItemModal form').addEventListener('submit', function () {
//         selectedModifiers = [];

//         document.querySelectorAll(".modifier-checkbox:checked").forEach(cb => {
//             const parent = cb.closest(".list-group-item");
//             const modifierId = parseInt(cb.dataset.id);
//             const mean = parseInt(parent.querySelector(".mean-select").value);
//             const max = parseInt(parent.querySelector(".max-select").value);

//             selectedModifiers.push({ ModifierId: modifierId, Mean: mean, Max: max });
//         });

//         if (selectedModifiers.length > 0) {
//             document.getElementById('SelectedModifiersJson').value = JSON.stringify(selectedModifiers);
//         }
//     });
// });
document.addEventListener("DOMContentLoaded", function () {
    const buttons = document.querySelectorAll(".modifier-btn");
    const listContainer = document.getElementById("modifierList");
    const wrapper = document.getElementById("modifierDetails");

    buttons.forEach(btn => {
        btn.addEventListener("click", function () {
            const groupId = this.dataset.groupId;

            fetch(`/SuperAdmin/GetModifiersByGroup?groupId=${groupId}`)
                .then(response => response.json())
                .then(data => {
                    listContainer.innerHTML = "";
                    if (data.length > 0) {
                        wrapper.style.display = "block";
                        data.forEach((mod, index) => {
                            const li = document.createElement("li");
                            li.className = "list-group-item";

                            li.innerHTML = `
                                <div class="d-flex align-items-center gap-2">
                                    <input type="checkbox" class="modifier-checkbox" data-index="${index}">
                                    <span>${mod.name} - ₹${mod.price}</span>
                                    <select class="form-select form-select-sm mean-select" style="width: 70px;">
                                        ${[...Array(101).keys()].map(i => `<option value="${i}">${i}</option>`).join('')}
                                    </select>
                                    <select class="form-select form-select-sm max-select" style="width: 70px;">
                                        ${[...Array(101).keys()].map(i => `<option value="${i}">${i}</option>`).join('')}
                                    </select>
                                    <input type="hidden" name="SelectedModifiers[${index}].ModifierId" value="${mod.modifierId}" />
                                    <input type="hidden" name="SelectedModifiers[${index}].Mean" class="mean-hidden" />
                                    <input type="hidden" name="SelectedModifiers[${index}].Max" class="max-hidden" />
                                </div>
                            `;

                            listContainer.appendChild(li);
                        });
                    } else {
                        wrapper.style.display = "none";
                    }
                });
        });
    });

    document.querySelector('#AddMenuItemModal form').addEventListener('submit', function () {
        document.querySelectorAll(".modifier-checkbox:checked").forEach(cb => {
            const parent = cb.closest(".list-group-item");
            const mean = parent.querySelector(".mean-select").value;
            const max = parent.querySelector(".max-select").value;
            parent.querySelector(".mean-hidden").value = mean;
            parent.querySelector(".max-hidden").value = max;
        });
    });
});
