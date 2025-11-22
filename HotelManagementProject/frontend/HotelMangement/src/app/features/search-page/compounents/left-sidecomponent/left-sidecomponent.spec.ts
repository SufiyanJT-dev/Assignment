import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeftSidecomponent } from './left-sidecomponent';

describe('LeftSidecomponent', () => {
  let component: LeftSidecomponent;
  let fixture: ComponentFixture<LeftSidecomponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeftSidecomponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeftSidecomponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
