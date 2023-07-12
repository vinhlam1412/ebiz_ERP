import { CoreModule } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgModule } from '@angular/core';
import {
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';
import { PageModule } from '@abp/ng.components/page';
import { CSAttributeDetailComponent } from './components/csattribute-detail.component';
import { CSAttributeDetailRoutingModule } from './csattribute-detail-routing.module';

@NgModule({
  declarations: [CSAttributeDetailComponent],
  imports: [
    CSAttributeDetailRoutingModule,
    CoreModule,
    ThemeSharedModule,
    CommercialUiModule,
    NgxValidateCoreModule,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbDropdownModule,

    PageModule,
  ],
})
export class CSAttributeDetailModule {}

export function loadCSAttributeDetailModuleAsChild() {
  return Promise.resolve(CSAttributeDetailModule);
}
