import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-bundle-preview',
  imports: [RouterLink],
  templateUrl: './bundle-preview.component.html',
  styleUrl: './bundle-preview.component.scss',
})
export class BundlePreviewComponent {
  @Input() bundle: any;
}
