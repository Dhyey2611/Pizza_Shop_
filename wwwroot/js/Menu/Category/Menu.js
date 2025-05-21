document.addEventListener('DOMContentLoaded', function () {
    const categoryItems = document.querySelectorAll('.category-item');

    categoryItems.forEach(item => {
        const popup = item.querySelector('.popup-menu');
        const label = item.querySelector('span');

        label.addEventListener('click', (e) => {
            document.querySelectorAll('.popup-menu').forEach(p => {
                if (p !== popup) p.style.display = 'none';
            });

            popup.style.display = (popup.style.display === 'flex') ? 'none' : 'flex';
            e.stopPropagation(); 
        });

        item.addEventListener('mouseenter', () => {
            popup.style.display = 'flex';
        });

        item.addEventListener('mouseleave', () => {
            popup.style.display = 'none';
        });
    });

    document.addEventListener('click', function (event) {
        document.querySelectorAll('.popup-menu').forEach(popup => {
            if (!popup.contains(event.target)) {
                popup.style.display = 'none';
            }
        });
    });
     // ✅ Additional Code for Active Category Highlight
     const categoryLinks = document.querySelectorAll(".category-item a");
     categoryLinks.forEach(link => {
         link.addEventListener("click", function () {
             categoryLinks.forEach(l => l.classList.remove("active-category")); // Remove from all
             this.classList.add("active-category"); // Add to clicked
         });
    });
});