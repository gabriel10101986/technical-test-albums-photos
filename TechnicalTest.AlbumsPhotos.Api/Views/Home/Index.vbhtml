@Code
    ViewData("Title") = "TechnicalTest AlbumsPhotos API"
End Code

<h2>TechnicalTest AlbumsPhotos API</h2>

<p>La API está en ejecución.</p>

<div class="row">
    <div class="col-md-6">
        <h2>Albums</h2>
        <ul>
            <li><strong>GET</strong> /api/albums</li>
            <li><strong>GET</strong> /api/albums/{id}</li>
            <li><strong>GET</strong> /api/albums/search?title=valor</li>
            <li><strong>POST</strong> /api/albums</li>
            <li><strong>PUT</strong> /api/albums/{id}</li>
            <li><strong>DELETE</strong> /api/albums/{id}</li>
        </ul>
    </div>

    <div class="col-md-6">
        <h2>Photos</h2>
        <ul>
            <li><strong>GET</strong> /api/photos</li>
            <li><strong>GET</strong> /api/photos/{id}</li>
            <li><strong>GET</strong> /api/photos/search?albumId=1&amp;title=valor</li>
            <li><strong>POST</strong> /api/photos</li>
            <li><strong>PUT</strong> /api/photos/{id}</li>
            <li><strong>DELETE</strong> /api/photos/{id}</li>
        </ul>
    </div>
</div>