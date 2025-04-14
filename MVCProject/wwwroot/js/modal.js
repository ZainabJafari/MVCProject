document.addEventListener("DOMContentLoaded", function () {
    const modal = document.getElementById("addProjectModal");
    const openBtn = document.getElementById("openModalBtn");
    const closeBtn = document.getElementById("closeModalBtn");

    if (openBtn && modal && closeBtn) {
        openBtn.onclick = function () {
            modal.style.display = "block";
        }

        closeBtn.onclick = function () {
            modal.style.display = "none";
        }

        window.onclick = function (event) {
            if (event.target === modal || event.target.classList.contains("modal-overlay")) {
                modal.style.display = "none";
            }
        }
    }
});
