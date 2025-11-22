import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RightSidecomponent } from './right-sidecomponent';

describe('RightSidecomponent', () => {
  let component: RightSidecomponent;
  let fixture: ComponentFixture<RightSidecomponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RightSidecomponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RightSidecomponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
